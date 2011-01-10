using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using Platform.CommonUtils;

namespace SocketUtils
{
    public delegate byte[] TcpCallback(byte[] byteArray, string address);

    public class TcpListenerX : TcpListener
    {
        private TcpCallback callback = null;
        public int Port { get; set; }
        public bool Running { get; set; }
        public TcpListenerX(IPAddress localaddr, int port, TcpCallback callback)
            : base(localaddr, port)
        {
            this.Port = port;
            this.callback = callback;
            this.Running = true;
        }

        public void Run()
        {
            this.Start();
            while (this.Running)
            {
                using (TcpClient client = this.AcceptTcpClient())
                {
                    using (NetworkStream stream = client.GetStream())
                    {
                        byte[] bytes = StreamUtils.ReadStream(client, stream);
                        bytes = this.callback(bytes, ((System.Net.IPEndPoint)(client.Client.RemoteEndPoint)).Address.ToString());
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
