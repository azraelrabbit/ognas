using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.OgnasEventArgs
{
    public class RecivedMessageArgs : EventArgs
    {
        public Protocals.UdpMessageProtocal Message
        {
            get;
            set;
        }
    }
}
