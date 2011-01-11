using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using Platform.Protocals;
using Platform.CommonUtils;


namespace Platform.SocketUtils
{
    public class TcpClientUtils
    {
        private string serverName = string.Empty;
        private int serverPort = 0;
        public TcpClientUtils(String server, int port)
        {
            if (string.IsNullOrWhiteSpace(server) || port < 1)
            {
                throw new ArgumentNullException();
            }
            this.serverName = server;
            this.serverPort = port;
        }

        public byte[] SendData(Protocal protocal)
        {
            return this.SendMessage(protocal.RequestData);
        }

        private byte[] SendData(byte message)
        {
            return this.SendMessage(new byte[] { message });
        }

        private byte[] SendData(byte[] message)
        {
            if (null == message || message.Length < 1)
            {
                throw new ArgumentNullException();
            }

            return this.SendMessage(message);
        }

        private byte[] SendMessage(byte[] message)
        {
            return SendMessage(this.serverName, this.serverPort, message);            
        }

        public static byte[] SendData(String server, int port, Protocal protocal)
        {
            return SendMessage(server, port, protocal.RequestData);
        }

        private static byte[] SendData(String server, int port, byte[] message)
        {
            if (null == message || message.Length < 1)
            {
                throw new ArgumentNullException();
            }

            return SendMessage(server, port, message);
        }

        private static byte[] SendMessage(String server, int port, byte[] message)
        {
            using (TcpClient client = new TcpClient(server, port))
            {
                using (NetworkStream stream = client.GetStream())
                {
                    stream.Write(message, 0, message.Length);

                    if (stream.CanRead)
                    {
                        return StreamUtils.ReadStream(client, stream);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
    }
}
