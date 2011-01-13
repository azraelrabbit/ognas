﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ognas.Lib.Protocols;

namespace Platform.Protocols
{
    public class ServerEnterRoomProtocol : EnterRoomProtocol
    {
        public ServerEnterRoomProtocol() : base()
        {            
        }

        public ServerEnterRoomProtocol(byte[] bytes)
            : base(bytes)
        {            
        }

        public override byte[] OnResponse()
        {
            MainFrame mainFrame = (MainFrame)this.Host;
            return mainFrame.EnterRoom(this);
        }
    }
}
