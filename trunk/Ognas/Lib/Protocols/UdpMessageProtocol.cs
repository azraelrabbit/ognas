using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ognas.Lib.Enums;

namespace Ognas.Lib.Protocols
{
    public class UdpMessageProtocol : Protocol
    {
        public UdpMessageProtocol()
        {
            this.SystemMessageEnum = SystemMessage.UdpMessage;
        }

        public UdpMessageProtocol(byte[] bytes)
        {
            this.SystemMessageEnum = SystemMessage.UdpMessage;
            this.Data = Encoding.UTF8.GetString(bytes);
        }

        public override byte[] OnResponse()
        {
            throw new NotImplementedException();
        }
    }
}
