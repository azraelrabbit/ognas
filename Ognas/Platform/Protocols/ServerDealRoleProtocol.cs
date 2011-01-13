using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Platform.Model;
using Ognas.Lib.Enums;
using Ognas.Lib.Protocols;
using Ognas.Lib;
using Ognas.Lib.Enum;

namespace Platform.Protocols
{
    public class ServerDealRoleProtocol : DealRoleProtocol
    {
        public ServerDealRoleProtocol() : base()
        {            
        }

        public ServerDealRoleProtocol(byte[] bytes)
            : base(bytes)
        {            
        }
    }
}
