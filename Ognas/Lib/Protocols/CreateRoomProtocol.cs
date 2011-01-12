using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ognas.Lib.Enums;

namespace Ognas.Lib.Protocols
{
    public class CreateRoomProtocol : Protocol
    {
        public CreateRoomProtocol()
        {
            this.SystemMessageEnum = SystemMessage.CreateRoom;
        }

        public CreateRoomProtocol(byte[] bytes)
        {
            this.SystemMessageEnum = SystemMessage.CreateRoom;
            this.Data = Encoding.UTF8.GetString(bytes);
        }

        public override byte[] OnResponse()
        {
            throw new NotImplementedException();
        }
    }
}
