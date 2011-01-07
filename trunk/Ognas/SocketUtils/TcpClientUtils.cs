using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace SocketUtils
{
    public class TcpClientUtils
    {
        public static byte[] SendData(String server, int port, byte[] message)
        {
            if (null == message || message.Length < 1)
            {
                throw new ArgumentNullException();
            }

            using(TcpClient client = new TcpClient(server, port))
            {
                using(NetworkStream stream = client.GetStream())
                {
                    stream.Write(message, 0, message.Length);

                    byte[] bytes = new byte[client.ReceiveBufferSize];
                    stream.Read(bytes, 0, bytes.Length);
                    return bytes;
                }
            }
        }
    }
}
