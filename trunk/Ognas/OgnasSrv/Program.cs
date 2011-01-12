using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Platform;
using SocketUtils;
using System.Net;
using System.Net.Sockets;

namespace Ognas.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                MainFrame mainFrame = new MainFrame();
                mainFrame.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("The following exception occurred in mainframe.");
                Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
                Console.ReadLine();
            }
            
        }
    }
}
