using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ognas.Lib.Protocols;

namespace Ognas.Client.Protocols
{
    public class ClientRegisterUserProtocol : RegisterUserProtocol
    {
        public ClientRegisterUserProtocol() : base()
        {            
        }

        public ClientRegisterUserProtocol(byte[] bytes)
            : base(bytes)
        {            
        }
    }
}
