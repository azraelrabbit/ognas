using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Platform.Model;
using Ognas.Lib.Enums;
using Ognas.Lib.Protocols;

namespace Platform.Protocols
{
    public class ServerExitRoomProtocol : ExitRoomProtocol
    {
        public ServerExitRoomProtocol() : base()
        {            
        }

        public ServerExitRoomProtocol(byte[] bytes)
            : base(bytes)
        {            
        }

        public override byte[] OnResponse()
        {
            Room room = (Room)Host;
            return room.UserExit(this);
        }
    }
}
