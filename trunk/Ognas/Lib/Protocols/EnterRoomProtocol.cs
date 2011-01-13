using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ognas.Lib.Enums;

namespace Ognas.Lib.Protocols
{
    public class EnterRoomProtocol : Protocol
    {
        public EnterRoomProtocol()
        {
            this.SystemMessageEnum = SystemMessage.EnterRoom;
        }

        public EnterRoomProtocol(byte[] bytes)
        {
            this.SystemMessageEnum = SystemMessage.EnterRoom;
            this.Data = Encoding.UTF8.GetString(bytes);
        }
    }
}
