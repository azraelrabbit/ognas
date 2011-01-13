using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ognas.Lib.Protocols;
using System.Windows.Threading;
using System.Threading;
using Ognas.Lib;
using System.Windows.Documents;
using Ognas.Client.Model;

namespace Ognas.Client
{
    public static class ProtocolProcess
    {
        public static byte[] DealRoleProtocolResponse(object host, Protocol protocol)
        {
            MainWindow mainWindow = (MainWindow)host;
            DealRoleProtocol dealRoleProtocol = (DealRoleProtocol)protocol;
            mainWindow.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate()
            {
                mainWindow.localUser.UserRole = dealRoleProtocol.player.UserRole;
                string m = mainWindow.localUser.UserRole.ToString();
                mainWindow.richMessage.AppendText("your role is : " + m + Environment.NewLine + " the Lord is : " + dealRoleProtocol.playerKing.UserName + Environment.NewLine);
            });
            return null;
        }

        public static byte[] DealSeatProtocolResponse(object host, Protocol protocol)
        {
            MainWindow mainWindow = (MainWindow)host;
            DealSeatProtocol dealSeatProtocol = (DealSeatProtocol)protocol;
            mainWindow.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate()
            {
                foreach (User u in dealSeatProtocol.userList)
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

        public static byte[] UdpMessageProtocolResponse(object host, Protocol protocol)
        {
            MainWindow mainWindow = (MainWindow)host;
            UdpMessageProtocol udpMessageProtocol = (UdpMessageProtocol)protocol;
            mainWindow.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate()
            {
                int index = udpMessageProtocol.Data.IndexOf(':');
                if (index > -1)
                {
                    Paragraph paragraph = new Paragraph();
                    Span span = new Span();
                    span.Foreground= Constants.NameColor;
                    span.Inlines.Add(udpMessageProtocol.Data.Substring(0, index + 1));
                    paragraph.Inlines.Add(span);
                    span = new Span();
                    span.Foreground = Constants.ContentColor;
                    span.Inlines.Add(udpMessageProtocol.Data.Substring(index + 1));
                    paragraph.Inlines.Add(span);

                    mainWindow.richMessage.Document.Blocks.Add(paragraph);
                }
                else
                {
                    mainWindow.richMessage.AppendText(udpMessageProtocol.Data + Environment.NewLine);
                }
            });
            return null;
        }
    }
}
