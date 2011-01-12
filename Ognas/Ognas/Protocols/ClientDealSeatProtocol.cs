using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ognas.Lib.Protocols;
using System.Windows.Threading;
using System.Threading;
using Ognas.Lib;

namespace Ognas.Client.Protocols
{
    public class ClientDealSeatProtocol : DealSeatProtocol
    {
        public override byte[] OnResponse()
        {
            MainWindow mainWindow = (MainWindow)this.Host;
            mainWindow.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate()
            {                
                foreach (User u in this.userList)
                {
                    if (u.Address == mainWindow.localUser.Address)
                    {
                        mainWindow.localUser = u;
                        break;
                    }
                }
                mainWindow.richMessage.AppendText("your seat number : " + mainWindow.localUser.SeatNum + Environment.NewLine);
            });
            return null;
        }
    }
}
