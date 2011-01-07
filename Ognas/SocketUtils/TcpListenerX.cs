using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace SocketUtils
{
    public delegate byte[] TcpCallback(byte[] byteArray, EndPoint endPoint);

    public class TcpListenerX : TcpListener
    {
        private TcpCallback callback = null;
        public int Port { get; set; }
        public TcpListenerX(IPAddress localaddr, int port, TcpCallback callback)
            : base(localaddr, port)
        {
            this.Port = port;
            this.callback = callback;
        }

        public void Run()
        {
            while (true)
            {
                using (TcpClient client = this.AcceptTcpClient())
                {
                    using (NetworkStream stream = client.GetStream())
                    {
                        byte[] bytes = new byte[client.ReceiveBufferSize];
                        stream.Read(bytes, 0, bytes.Length);
                        bytes = this.callback(bytes, client.Client.RemoteEndPoint);
                        if (null != bytes && bytes.Length > 0)
                        {
                            stream.Write(bytes, 0, bytes.Length);                            
                        }
                    }
                }
            }
        }
    }
}
