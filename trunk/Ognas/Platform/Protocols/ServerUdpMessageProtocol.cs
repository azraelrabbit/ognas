using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Platform.Model;
using Ognas.Lib.Enums;
using Ognas.Lib.Protocols;

namespace Platform.Protocols
{
    public class ServerUdpMessageProtocol : UdpMessageProtocol
    {
        public ServerUdpMessageProtocol() : base()
        {            
        }

        public ServerUdpMessageProtocol(byte[] bytes)
            : base(bytes)
        {            

        }
        public override byte[] OnResponse()
        {
            Room room = (Room)Host;
            string userName = room.AddressUserDictionary[this.ClientAddress].UserName;
            this.Data = string.Format("{0} : {1}", userName, this.Data);
            return room.SendUdpMessage(this.Data);
        }
    }
}
