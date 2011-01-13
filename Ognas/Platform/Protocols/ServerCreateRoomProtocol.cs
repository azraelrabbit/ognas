using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ognas.Lib.Protocols;

namespace Platform.Protocols
{
    public class ServerCreateRoomProtocol : CreateRoomProtocol
    {
        public ServerCreateRoomProtocol() : base()
        {            
        }

        public ServerCreateRoomProtocol(byte[] bytes)
            : base(bytes)
        {            
        }

        public override byte[] OnResponse()
        {
            MainFrame mainFrame = (MainFrame)this.Host;
            return mainFrame.CreateRoomThread(this);
        }
    }
}
