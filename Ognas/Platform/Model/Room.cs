using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocketUtils;
using System.Net.Sockets;
using Platform.Enum;
using System.Net;
using Platform.SocketUtils;
using Platform.Protocals;

namespace Platform.Model
{
    public class Room : IDisposable
    {
        private TcpListenerX tcpListenerX = null;

        private int roomTcpPort = 0;

        private Dictionary<string, User> userDoctionary = new Dictionary<string, User>();

        private string roomName;

        private int userMaxCount = 8;

        private User roomCreator = null;

        private object roomLock = new object();

        public RoomEnd RoomEnd = null;

        public bool IsFull 
        {
            get
            {
                return userDoctionary.Count == userMaxCount;
            }
        }

        public string RoomName
        {
            get { return roomName; }
        }

        public int RoomTcpPort
        {
            get
            {
                return this.roomTcpPort;
            }
        }

        public Room(string roomName, User user)
        {
            this.roomName = roomName;
            this.roomTcpPort = RoomPort.GetRoomTcpPort();
            this.roomCreator = user;
            this.AddUser(user);
            Console.WriteLine(string.Format("The room {0} is created", this.roomName));
        }

        internal void Start()
        {
            Console.WriteLine(string.Format("The room {0} is running", this.roomName));
            this.tcpListenerX = new TcpListenerX(MainFrame.SystemIPAddress, this.roomTcpPort, ReceiveTcpMessage);            
            tcpListenerX.Run();
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
            return null;
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (null != this.tcpListenerX)
            {
                this.tcpListenerX.Stop();
            }

            foreach (var user in this.userDoctionary.Values)
            {
                user.Room = null;
            }
        }

        #endregion

        internal void AddUser(User user)
        {
            lock (roomLock)
            {
                userDoctionary.Add(user.Address, user);
                foreach (var userItem in userDoctionary.Values)
                {
                    this.SendUdpMessage(string.Format("User {0} has entered the room {1}.", user.UserName, this.roomName));                    
                }

                if (IsFull)
                {
                    // initialize game
                }
            }
        }

        internal byte[] UserExit(Protocal protocal)
        {
            lock (roomLock)
            {
                // notify play
                // send Udp message
                this.SendUdpMessage(string.Format("the user {0} has exited the room {1}.", this.userDoctionary[protocal.ClientAddress].UserName, this.roomName));
                if (this.userDoctionary.Count< 1 && null != this.RoomEnd)
                {
                    this.RoomEnd(this);
                }
                return null;
            }

        }

        internal void SendUdpMessage(string message)
        {
            foreach (var address in userDoctionary.Keys)
            {
                Protocal protocal = new UdpMessageProtocal();
                protocal.Data = message;
                TcpClientUtils.SendData(address, Constants.ClientPort, protocal.RequestData);
            }
        }
    }
}
