using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace SocketUtils
{
    public delegate void UdpCallback(string message);

    public class UdpListenerX : UdpClient
    {
        private UdpCallback callback = null;
        private int port = 0;

        private IPEndPoint groupEP = null;

        public bool Running = true;
        public UdpListenerX(IPAddress localaddr, int port, UdpCallback callback)
            : base(port)
        {
            this.port = port;
            this.callback = callback;
            groupEP = new IPEndPoint(localaddr, port);
        }

        public void Start()
        {
            while (this.Running)
            {
                byte[] bytes = this.Receive(ref groupEP);

                string message = Encoding.UTF8.GetString(bytes);

                this.callback(message);                    
            }
        }
    }
}
