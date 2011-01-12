using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Platform.Model;
using Ognas.Lib.Enums;
using Ognas.Lib.Protocols;

namespace Platform.Protocols
{
    public class ServerUdpMessageProtocol : UdpMessageProtocol
    {
        public override byte[] OnResponse()
        {
            Room room = (Room)Host;
            return room.SendUdpMessage(this.Data);
        }
    }
}
