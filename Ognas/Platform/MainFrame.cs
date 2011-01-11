using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocketUtils;
using System.Net;
using System.Configuration;
using System.Threading;
using Platform.Enum;
using System.Net.Sockets;
using Platform.Model;
using Platform.Protocals;
using Platform.CommonUtils;

namespace Platform
{
    public class MainFrame : IDisposable
    {
        public static int ServerPort = Convert.ToInt32(ConfigurationManager.AppSettings["ServerPort"]);

        public static IPAddress SystemIPAddress = Dns.GetHostAddresses(Dns.GetHostName())[0];

        private TcpListenerX tcpListenerX = null;

        private Dictionary<string, Room> roomDictionary = new Dictionary<string, Room>();

        private Dictionary<string, User> userDictionary = new Dictionary<string, User>();

        private Dictionary<string, User> AddressDictionary = new Dictionary<string, User>();

        public object maimFrameLock = new object();

        public MainFrame()
        {            
            
        }

        public void Start()
        {
            Console.WriteLine("The main frame started and enter message loop.");
            this.tcpListenerX = new TcpListenerX(SystemIPAddress, ServerPort, ReceiveTcpMessage);
            this.tcpListenerX.Run();
        }

        public byte[] ReceiveTcpMessage(byte[] bytes, string address)
        {
            try
            {
                if (null != bytes && bytes.Length > 0)
                {
                    Protocal protocal = ProtocalFactory.CreateProtocal(bytes);
                    protocal.Host = this;
                    protocal.ClientAddress = address;
                    return protocal.OnResponse();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("An exception occurred when receive message. {0}", CommonUtil.GetOutputExceptionWithNewLine(ex)));
            }
            return null;
        }

        public byte[] CreateRoomThread(Protocal protocal)
        {
            try
            {
                ProtocalParam protocalParam = new ProtocalParam();
                protocalParam.Protocal = protocal;
                Thread thread = new Thread(new ParameterizedThreadStart(CreateRoom));
                thread.IsBackground = true;                
                thread.Start(protocalParam);
                while (null == protocalParam.Param)
                {
                    Thread.Sleep(100);
                }
                return BitConverter.GetBytes((int)protocalParam.Param);
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Create room thread failed. {0}", CommonUtil.GetOutputExceptionWithNewLine(ex)));
            }
            return null;            
        }

        public void CreateRoom(object obj)
        {
            int port = 0;
            ProtocalParam protocalParam = (ProtocalParam)obj;
            string roomName = (string)protocalParam.Protocal.Data;
            Room room = null;
            try
            {
                User user = this.AddressDictionary[protocalParam.Protocal.ClientAddress];
                lock (this.maimFrameLock)
                {
                    if (!string.IsNullOrWhiteSpace(roomName) && 
                        (!this.roomDictionary.ContainsKey(roomName) || null == this.roomDictionary[roomName]))
                    {
                        room = new Room(roomName, user);
                        room.RoomEnd = this.DisposeRoom;
                        user.Room = room;
                        this.roomDictionary.Add(roomName, room);
                        port = room.RoomTcpPort;
                    }
                }

                protocalParam.Param = port;
                if (null != room)
                {
                    room.Start();
                }
            }
            catch
            {
                try 
	            {	        
		            if (null != room)
                    {
                        room.Dispose();
                        room = null;
                        if (this.roomDictionary.ContainsKey(roomName))
                        {
                            this.roomDictionary.Remove(roomName);
                        }
                    }                        
	            }
	            catch
	            {
	            }
            }
        }

        public byte[] EnterRoom(Protocal protocal)
        {
            int port = 0;
            string roomName = protocal.Data;
            if (!string.IsNullOrWhiteSpace(roomName) && this.roomDictionary.ContainsKey(roomName) && !this.roomDictionary[roomName].IsFull)
            {
                User user = this.AddressDictionary[protocal.ClientAddress];
                user.Room = this.roomDictionary[roomName];
                this.roomDictionary[roomName].AddUser(user);
                port = this.roomDictionary[roomName].RoomTcpPort;
            }
            return BitConverter.GetBytes(port);
        }

        public byte[] RegisterUser(Protocal protocal)
        {
            bool rtn = true;
            string userName = protocal.Data;
            if (this.userDictionary.ContainsKey(userName))
            {
                // update user address to avoiding user address change
                if(protocal.ClientAddress != this.userDictionary[userName].Address)
                {
                    this.AddressDictionary.Remove(this.userDictionary[userName].Address);
                    if (this.AddressDictionary.ContainsKey(protocal.ClientAddress))
                    {
                        this.AddressDictionary[protocal.ClientAddress] = this.userDictionary[userName];
                    }
                    else
                    {
                        this.AddressDictionary.Add(protocal.ClientAddress, this.userDictionary[userName]);
                    }
                }

            }
            else
            {
                User user = new User(userName);
                user.Address = protocal.ClientAddress;
                this.userDictionary.Add(userName, user);
                if (this.AddressDictionary.ContainsKey(user.Address))
                {
                    this.AddressDictionary[user.Address] = user;
                }
                else
                {
                    this.AddressDictionary.Add(user.Address, user);
                }
            }
            return BitConverter.GetBytes(rtn);
        }

        public void DisposeRoom(Room room)
        {
            this.roomDictionary.Remove(room.RoomName);
            room.Dispose();
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (null != this.tcpListenerX)
            {
                this.tcpListenerX.Stop();
            }
        }
        #endregion
    }
}
