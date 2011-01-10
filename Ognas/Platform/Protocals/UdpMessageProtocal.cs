using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Protocals
{
    public class UdpMessageProtocal : Protocal
    {
        public UdpMessageProtocal()
        {
            this.SystemMessageEnum = Enum.SystemMessage.UdpMessage;
        }

        public UdpMessageProtocal(byte[] bytes)
        {
            this.SystemMessageEnum = Enum.SystemMessage.UdpMessage;
            this.Data = Encoding.UTF8.GetString(bytes);
        }

        public override byte[] OnResponse()
        {
            throw new NotImplementedException();
        }
    }
}
