using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocketUtils;
using System.Net.Sockets;
using Platform.Enum;
using System.Net;

namespace Platform
{
    public class House : IDisposable
    {
        private TcpListenerX tcpListenerX = null;

        private UdpClient udpClient = null;

        private int houseTcpPort = HousePort.GetHouseTcpPort();

        private int houseUdpPort = HousePort.GetHouseUdpPort();


        public int HouseTcpPort
        {
            get
            {
                return this.houseTcpPort;
            }
            set
            {
                this.houseTcpPort = value;
            }
        }

        public House()
        {
            Console.WriteLine("a new house is created.");
        }

        internal void Start()
        {
            Console.WriteLine("The house start");
            tcpListenerX = new TcpListenerX(MainFrame.SystemIPAddress, this.houseTcpPort, ReceiveTcpMessage);
            tcpListenerX.Start();
            tcpListenerX.Run();
            udpClient = new UdpClient();
            udpClient.Connect(MainFrame.UdpIPAddress, this.houseUdpPort);
        }

        public byte[] ReceiveTcpMessage(byte[] bytes, EndPoint endPoint)
        {
            if (null != bytes && bytes.Length > 0)
            {
                if ((byte)SystemMessage.EnterHouse == bytes[0])
                {
                    Console.WriteLine(string.Format("{0} has entered the house.", endPoint.ToString()));
                }
            }
            return null;
        }

        #region IDisposable Members

        public void Dispose()
        {
            tcpListenerX.Stop();
            udpClient.Close();
        }

        #endregion
    }
}
