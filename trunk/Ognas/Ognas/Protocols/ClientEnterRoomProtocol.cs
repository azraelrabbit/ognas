using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ognas.Lib.Protocols;

namespace Ognas.Client.Protocols
{
    public class ClientEnterRoomProtocol : EnterRoomProtocol
    {
        public ClientEnterRoomProtocol() : base()
        {            
        }

        public ClientEnterRoomProtocol(byte[] bytes)
            : base(bytes)
        {            
        }
    }
}
