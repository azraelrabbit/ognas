using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Configuration;
using System.Threading;
using System.Net.Sockets;
using Platform.Model;
using Ognas.Lib.Protocols;
using Ognas.Lib;
using Ognas.Lib.SocketUtils;
using Ognas.Lib.CommonUtils;
using System.Reflection;

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

        private Dictionary<string, Room> UserRoomDictionary = new Dictionary<string, Room>();

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
                    Protocol protocol = ProtocolFactory.CreateProtocol(Assembly.GetExecutingAssembly(), bytes);
                    protocol.Host = this;
                    protocol.ClientAddress = address;
                    return protocol.OnResponse();                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("An exception occurred when receive message. {0}", CommonUtil.GetOutputExceptionWithNewLine(ex)));
            }
            return null;
        }

        public byte[] CreateRoomThread(Protocol protocol)
        {
            try
            {
                ProtocolParam protocolParam = new ProtocolParam();
                protocolParam.Protocol = protocol;
                Thread thread = new Thread(new ParameterizedThreadStart(CreateRoom));
                thread.IsBackground = true;                
                thread.Start(protocolParam);
                while (null == protocolParam.Param)
                {
                    Thread.Sleep(100);
                }
                return BitConverter.GetBytes((int)protocolParam.Param);
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
            ProtocolParam protocolParam = (ProtocolParam)obj;
            string roomName = (string)protocolParam.Protocol.Data;
            Room room = null;
            try
            {
                User user = this.AddressDictionary[protocolParam.Protocol.ClientAddress];
                lock (this.maimFrameLock)
                {
                    if (!string.IsNullOrWhiteSpace(roomName) && 
                        (!this.roomDictionary.ContainsKey(roomName) || null == this.roomDictionary[roomName]))
                    {
                        room = new Room(roomName, user);
                        room.RoomEnd = this.DisposeRoom;
                        if (this.UserRoomDictionary.ContainsKey(user.UserName))
                        {
                            this.UserRoomDictionary[user.UserName] = room;
                        }
                        else
                        {
                            this.UserRoomDictionary.Add(user.UserName, room);
                        }
                        this.roomDictionary.Add(roomName, room);
                        port = room.RoomTcpPort;
                    }
                }

                protocolParam.Param = port;
                if (null != room)
                {
                    room.Start();
                }
                Console.WriteLine(string.Format("The room {0} has ended.", roomName));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
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

        public byte[] EnterRoom(Protocol protocol)
        {
            int port = 0;
            string roomName = protocol.Data;
            if (!string.IsNullOrWhiteSpace(roomName) && this.roomDictionary.ContainsKey(roomName) && !this.roomDictionary[roomName].IsFull)
            {
                User user = this.AddressDictionary[protocol.ClientAddress];
                if (this.UserRoomDictionary.ContainsKey(user.UserName))
                {
                    this.UserRoomDictionary[user.UserName] = this.roomDictionary[roomName];
                }
                else
                {
                    this.UserRoomDictionary.Add(user.UserName, this.roomDictionary[roomName]);
                }
                this.roomDictionary[roomName].AddUser(user);
                port = this.roomDictionary[roomName].RoomTcpPort;
            }
            return BitConverter.GetBytes(port);
        }

        public byte[] RegisterUser(Protocol protocol)
        {
            bool rtn = true;
            string userName = protocol.Data;
            if (this.userDictionary.ContainsKey(userName))
            {
                // update user address to avoiding user address change
                if(protocol.ClientAddress != this.userDictionary[userName].Address)
                {
                    this.AddressDictionary.Remove(this.userDictionary[userName].Address);
                    if (this.AddressDictionary.ContainsKey(protocol.ClientAddress))
                    {
                        this.AddressDictionary[protocol.ClientAddress] = this.userDictionary[userName];
                    }
                    else
                    {
                        this.AddressDictionary.Add(protocol.ClientAddress, this.userDictionary[userName]);
                    }
                }

            }
            else
            {
                User user = new User(userName);
                user.Address = protocol.ClientAddress;
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
            if (null != room)
            {
                if (this.roomDictionary.ContainsKey(room.RoomName))
                {
                    this.roomDictionary.Remove(room.RoomName);
                }
                room.Dispose();
            }
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
