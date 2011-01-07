using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SocketUtils;
using System.Configuration;
using System.Net;
using Platform.Enum;

namespace Ognas.Client
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public TcpListenerX tcpListenerX = null;
        public string serverName = ConfigurationManager.AppSettings["ServerName"];
        public int serverPort = Convert.ToInt32(ConfigurationManager.AppSettings["ServerPort"]);
        

        public static IPAddress SystemIPAddress = Dns.GetHostAddresses(Dns.GetHostName())[0];

        

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void btnEnterHouse_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHousePort.Text))
            {
                return;
            }
            List<byte> byteList = new List<byte>();
            byteList.Add((byte)SystemMessage.EnterHouse);

            int port = Convert.ToInt32(txtHousePort.Text);
            byteList.AddRange(BitConverter.GetBytes(port));
            TcpClientUtils.SendData(this.serverName, this.tcpListenerX.Port, byteList.ToArray());
        }

        private void btnCreateHouse_Click(object sender, RoutedEventArgs e)
        {
            TcpClientUtils.SendData(this.serverName, this.serverPort, new byte[] { (byte)SystemMessage.CreateHouse });
        }

        public byte[] ReceiveTcpMessage(byte[] bytes, EndPoint endPoint)
        {
            if (null != bytes && bytes.Length > 0)
            {
                if ((byte)SystemMessage.ServerHousePort == bytes[0])
                {
                    int port = BitConverter.ToInt32(bytes, 1);
                    CreateTcpListener(port);
                }
            }
            return null;
        }

        public void CreateTcpListener(int port)
        {
            tcpListenerX = new TcpListenerX(SystemIPAddress, this.serverPort, ReceiveTcpMessage);
        }
    }
}
