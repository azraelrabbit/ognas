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
            this.tcpListenerX = new TcpListenerX(SystemIPAddress, ServerPort, ReceiveTcpMessage);
            this.tcpListenerX.Run();            
        }

        public byte[] ReceiveTcpMessage(byte[] bytes, string address)
        {
            if (null != bytes && bytes.Length > 0)
            {
                Protocal protocal = ProtocalFactory.CreateProtocal(bytes);
                protocal.Host = this;
                protocal.ClientAddress = address;
                return protocal.OnResponse();
            }
            else
            {
                return null;
            }
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
                Console.WriteLine(ex.ToString());
            }
            return null;
            
        }

        public void CreateRoom(object obj)
        {
            int port = 0;
            ProtocalParam protocalParam = (ProtocalParam)obj;
            string roomName = (string)protocalParam.Protocal.Data;
            User user = this.AddressDictionary[protocalParam.Protocal.ClientAddress];
            Room room = null;
            lock (this.maimFrameLock)
            {
                if (!string.IsNullOrWhiteSpace(roomName) && !this.roomDictionary.ContainsKey(roomName))
                {
                    room = new Room(roomName, user);
                    user.Room = room;
                    port = room.RoomTcpPort;                    
                }
            }

            protocalParam.Param = port;
            if (null != room)
            {
                room.Start();
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
