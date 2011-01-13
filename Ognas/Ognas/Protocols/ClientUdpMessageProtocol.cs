using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ognas.Lib.Protocols;
using System.Windows.Threading;
using System.Threading;

namespace Ognas.Client.Protocols
{
    public class ClientUdpMessageProtocol : UdpMessageProtocol
    {
        public ClientUdpMessageProtocol() : base()
        {            
        }

        public ClientUdpMessageProtocol(byte[] bytes)
            : base(bytes)
        {            
        }

        public override byte[] OnResponse()
        {
            MainWindow mainWindow = (MainWindow)this.Host;
            mainWindow.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate()
            {
                mainWindow.richMessage.AppendText(this.Data + Environment.NewLine);
            });
            return null;
        }
    }
}
