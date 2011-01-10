using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Model.OgnasEventArgs
{
    public class SendMessageArgs
    {
        public Protocals.UdpMessageProtocal Message
        {
            get;
            set;
        }
    }
}
