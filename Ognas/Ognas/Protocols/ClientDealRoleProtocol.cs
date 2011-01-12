using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ognas.Lib.Protocols;
using System.Windows.Threading;
using System.Threading;

namespace Ognas.Client.Protocols
{
    public class ClientDealRoleProtocol : DealRoleProtocol
    {
        public override byte[] OnResponse()
        {
            MainWindow mainWindow = (MainWindow)this.Host;
            mainWindow.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate()
            {
                mainWindow.localUser.UserRole = this.player.UserRole;
                string m = mainWindow.localUser.UserRole.ToString();
                mainWindow.richMessage.AppendText("your role is : " + m + Environment.NewLine + " the Lord is : " + this.playerKing.UserName + Environment.NewLine);
            });
            return null;
        }
    }
}
