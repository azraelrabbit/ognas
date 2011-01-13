using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ognas.Lib.Enums;

namespace Ognas.Lib.Protocols
{
    public class ExitRoomProtocol : Protocol
    {
        public ExitRoomProtocol()
        {
            this.SystemMessageEnum = SystemMessage.ExitRoom;
        }

        public ExitRoomProtocol(byte[] bytes)
        {
            this.SystemMessageEnum = SystemMessage.ExitRoom;
            this.Data = Encoding.UTF8.GetString(bytes);
        }
    }
}
