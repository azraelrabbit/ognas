using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ognas.Lib.Protocols;

namespace Platform.OgnasEventArgs
{
    public class SendMessageArgs : EventArgs
    {
        public UdpMessageProtocol Message
        {
            get;
            set;
        }
    }
}
