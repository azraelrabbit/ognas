using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using Platform.Protocols;
using Platform.Games;
using Ognas.Lib.Protocols;
using Ognas.Lib;
using Ognas.Lib.SocketUtils;
using Ognas.Lib.CommonUtils;

namespace Platform.Model
{
    public class Room : IDisposable
    {
        private TcpListenerX tcpListenerX = null;

        private int roomTcpPort = 0;

        private Dictionary<string, User> addressUserDoctionary = new Dictionary<string, User>();

        private string roomName;

        private int userMaxCount = 2;

        private User roomCreator = null;

        private object roomLock = new object();

        public RoomEnd RoomEnd = null;

        private GameBase gameBase = null;

        public bool IsFull
        {
            get
            {
                return addressUserDoctionary.Count == userMaxCount;
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
            try
            {
                if (null != bytes && bytes.Length > 0)
                {
                    Protocol protocol = ProtocolFactory.CreateProtocol(bytes);
                    protocol.Host = this;
                    protocol.ClientAddress = address;
                    return protocol.OnResponse();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("An exception occurred when receive message in the room {0}. {1}", this.RoomName, CommonUtil.GetOutputExceptionWithNewLine(ex)));
            }
            return null;
        }

        #region IDisposable Members

        public void Dispose()
        {
            RoomPort.ReleasePort(this.roomTcpPort);

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
                addressUserDoctionary.Add(user.Address, user);
                this.SendUdpMessage(string.Format("User {0} has entered the room {1}.", user.UserName, this.roomName));

                if (IsFull)
                {
                    // initialize game
                    this.gameBase = new Games.Game(addressUserDoctionary.Values.ToList());
                    this.gameBase.GameStart();
                }
            }
        }

        internal byte[] UserExit(Protocol protocol)
        {
            lock (roomLock)
            {
                // notify play
                // send Udp message
                this.SendUdpMessage(string.Format("the user {0} has exited the room {1}.", this.addressUserDoctionary[protocol.ClientAddress].UserName, this.roomName));
                // trigger room exit event.
                if (this.addressUserDoctionary.Count < 1 && null != this.RoomEnd)
                {
                    this.RoomEnd(this);
                }
                return null;
            }

        }

        internal byte[] SendUdpMessage(string message)
        {
            foreach (var address in addressUserDoctionary.Keys)
            {
                Protocol protocol = new ServerUdpMessageProtocol();
                protocol.Data = message;
                TcpClientUtils.SendData(address, Constants.ClientPort, protocol);
            }
            return null;
        }

        internal byte[] SendMessageAll(Protocol message)
        {
            foreach (var address in addressUserDoctionary.Keys)
            {
                TcpClientUtils.SendData(address, Constants.ClientPort, message);
            }
            return null;
        }

        internal byte[] SendMessage(Protocol message)
        {
            TcpClientUtils.SendData(message.ClientAddress, Constants.ClientPort, message);
            return null;
        }
    }
}
