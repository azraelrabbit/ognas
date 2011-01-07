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

namespace Platform
{
    public class MainFrame
    {
        public TcpListenerX tcpListenerX = null;
        public int port = Convert.ToInt32(ConfigurationManager.AppSettings["ServerPort"]);

        public static IPAddress SystemIPAddress = Dns.GetHostAddresses(Dns.GetHostName())[0];
        //public static IPAddress SystemIPAddress = IPAddress.Parse("127.0.0.1");

        public static IPAddress UdpIPAddress = IPAddress.Parse(ConfigurationManager.AppSettings["UdpIPAddress"]);

        public List<House> houseList = new List<House>();

        public MainFrame()
        {
            tcpListenerX = new TcpListenerX(SystemIPAddress, this.port, ReceiveTcpMessage);
        }

        public void Start()
        {
            tcpListenerX.Start();
            tcpListenerX.Run();
            
        }

        public byte[] ReceiveTcpMessage(byte[] bytes, EndPoint endPoint)
        {
            if (null != bytes && bytes.Length > 0)
            {
                if ((byte)SystemMessage.CreateHouse == bytes[0])
                {
                    ThreadParam threadParam = new ThreadParam();
                    CreateHouseThread(threadParam);
                    while (null == threadParam.Param)
                    {
                        Thread.Sleep(100);
                    }
                    List<byte> byteList = new List<byte>();
                    byteList.Add((byte)SystemMessage.ServerHousePort);
                    byteList.AddRange(BitConverter.GetBytes(Convert.ToInt32(threadParam.Param.ToString())));
                    return byteList.ToArray();
                }
            }
            return null;
        }

        public void CreateHouseThread(ThreadParam threadParam)
        {
            try
            {
                Thread thread = new Thread(new ParameterizedThreadStart(CreateHouse));
                thread.IsBackground = true;                
                thread.Start(threadParam);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
        }

        public void CreateHouse(object threadParam)
        {
            House house = new House();
            ((ThreadParam)threadParam).Param = house.HouseTcpPort;
            houseList.Add(house);
            house.Start();
        }
    }
}
