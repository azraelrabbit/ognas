using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace SocketUtils
{
    public class UdpClientUtils
    {
        public static void Send(UdpClient udpClient, string message)
        {
            Byte[] sendBytes = Encoding.UTF8.GetBytes(message);
            udpClient.Send(sendBytes, sendBytes.Length); 
        }

        public static string ReceiveMessage(IPAddress ipAddress, int port)
        {
            using (UdpClient udpClient = new UdpClient())
            {
                IPEndPoint RemoteIpEndPoint = new IPEndPoint(ipAddress, port);
                Byte[] receiveBytes = udpClient.Receive(ref RemoteIpEndPoint);
                return Encoding.UTF8.GetString(receiveBytes); 
            }
        }
    }
}
