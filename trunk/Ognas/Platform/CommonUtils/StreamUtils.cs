using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Sockets;

namespace Platform.CommonUtils
{
    public static class StreamUtils
    {
        public static byte[] ReadStream(TcpClient tcpClient, Stream stream)
        {
            byte[] bytes = new byte[tcpClient.ReceiveBufferSize];
            int count = stream.Read(bytes, 0, (int)tcpClient.ReceiveBufferSize);
            return bytes.Take(count).ToArray();
        }
    }
}
