using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Ognas.Lib.Enums;

namespace Ognas.Lib.Protocols
{
    public abstract class Protocol
    {
        public SystemMessage SystemMessageEnum { get; set; }

        public object Host { get; set; }

        public string ClientAddress { get; set; }

        public string Data { get; set; }

        public bool ReturnValue { get; set; }

        public virtual byte[] RequestData
        {
            get
            {
                List<byte> byteList = new List<byte>();
                byteList.Add((byte)this.SystemMessageEnum);
                byteList.AddRange(Encoding.UTF8.GetBytes(this.Data));
                return byteList.ToArray();
            }
        }

        public virtual byte[] ResponseData
        {
            get
            {
                return new Byte[] { ReturnValue == true ? (byte)1 : (byte)0 };
            }
        }

        public abstract byte[] OnResponse();
        
    }
}
