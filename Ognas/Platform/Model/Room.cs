using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocketUtils;
using System.Net.Sockets;
using Platform.Enum;
using System.Net;

namespace Platform.Model
{
    public class Room : IDisposable
    {
        private TcpListenerX tcpListenerX = null;

        private int roomTcpPort = 0;

        private List<User> userList = new List<User>();

        private string roomName;

        private int userMaxCount = 8;

        private User roomCreator = null;

        private object roomLock = new object();

        public bool IsFull 
        {
            get
            {
                return userList.Count == userMaxCount;
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
            this.userList.Add(user);
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
                //if ((byte)SystemMessage.EnterRoom == bytes[0])
                //{
                //    Console.WriteLine(string.Format("{0} has entered the room.", endPoint.ToString()));
                //}
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
        }

        #endregion

        internal void AddUser(User user)
        {
            lock (roomLock)
            {
                userList.Add(user);
            }
        }

        
    }
}
