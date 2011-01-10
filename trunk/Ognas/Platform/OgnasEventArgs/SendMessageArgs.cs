using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.OgnasEventArgs
{
    public class SendMessageArgs : EventArgs
    {
        public Protocals.UdpMessageProtocal Message
        {
            get;
            set;
        }
    }
}
