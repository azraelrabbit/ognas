using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ognas.Lib.Protocols;

namespace Ognas.Client.Protocols
{
    public class ClientCreateRoomProtocol : CreateRoomProtocol
    {
        public ClientCreateRoomProtocol() : base()
        {            
        }

        public ClientCreateRoomProtocol(byte[] bytes)
            : base(bytes)
        {            
        }
    }
}
