﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ognas.Lib.Enums;

namespace Ognas.Lib.Protocols
{
    public class RegisterUserProtocol : Protocol
    {
        public RegisterUserProtocol()
        {
            this.SystemMessageEnum = SystemMessage.RegisterUserName;
        }

        public RegisterUserProtocol(byte[] bytes) 
        {
            this.SystemMessageEnum = SystemMessage.RegisterUserName;
            this.Data = Encoding.UTF8.GetString(bytes);
        }

        public override byte[] OnResponse()
        {
            throw new NotImplementedException();
        }
    }
}
