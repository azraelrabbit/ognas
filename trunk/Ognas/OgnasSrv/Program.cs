using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Platform;
using SocketUtils;
using System.Net;
using Platform.Enum;
using System.Net.Sockets;

namespace Ognas.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            MainFrame mainFrame = new MainFrame();
            mainFrame.Start();            
        }
    }
}
