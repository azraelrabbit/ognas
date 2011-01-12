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
using System.Configuration;
using System.Net;
using System.Threading;
using Ognas.Client.Model;
using System.Windows.Threading;
using Ognas.Lib;
using Ognas.Lib.Protocols;
using Ognas.Client.Protocols;
using Ognas.Lib.SocketUtils;

namespace Ognas.Client
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string ServerName = ConfigurationManager.AppSettings["ServerName"];
        public static int ServerPort = Convert.ToInt32(ConfigurationManager.AppSettings["ServerPort"]);
        public static TcpClientUtils TcpClientSystem = new TcpClientUtils(ServerName, ServerPort);
        public TcpClientUtils TcpClientRoom = null;

        public static IPAddress SystemIPAddress = Dns.GetHostAddresses(Dns.GetHostName())[0];

        public int currentRoomPort = 0;
        public User localUser = null;

        public MainWindow()
        {
            InitializeComponent();
            PlatFormInitialize();
        }

        public void PlatFormInitialize()
        {
            CreateTcpListenerThread();
            ShowUserNameWindow();
        }

        public void CreateTcpListenerThread()
        {
            Thread thread = new Thread(CreateTcpListenerX);
            thread.IsBackground = true;
            thread.Start();
        }

        public void CreateTcpListenerX()
        {
            try
            {
                TcpListenerX tcpListenerX = new TcpListenerX(SystemIPAddress, Constants.ClientPort, this.ReceiveTcpMessage);
                tcpListenerX.Run();
            }
            catch (Exception ex)
            {
                this.Dispatcher.BeginInvoke((ThreadStart)delegate
                {
                    throw ex;
                });
            }
            
        }

        public void ShowUserNameWindow()
        {
            var dialog = new UserNameWindow();
            if (dialog.ShowDialog() == true)
            {
                lblWelcomeInfo.Content = string.Format("Welcome: {0}. ", dialog.ResponseText);
                localUser = new User(dialog.ResponseText);
            }
            else
            {
                this.Close();
            }
        }

        private void btnEnterRoom_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRoomName.Text))
            {
                return;
            }

            ClientEnterRoomProtocol protocol = new ClientEnterRoomProtocol();
            protocol.Data = txtRoomName.Text;

            this.EnterRoom(protocol);
        }

        private void btnCreateRoom_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRoomName.Text))
            {
                return;
            }

            ClientCreateRoomProtocol protocol = new ClientCreateRoomProtocol();
            protocol.Data = txtRoomName.Text;

            this.EnterRoom(protocol);

        }

        private void EnterRoom(Protocol protocol)
        {
            int port = BitConverter.ToInt32(MainWindow.TcpClientSystem.SendData(protocol), 0);
            if (0 == port)
            {
                lblError.Content = "The name you entered is not existed.";
            }
            else
            {
                this.currentRoomPort = port;
                this.lblRoomInfo.Content = string.Format("You have enter the room {0}.", txtRoomName.Text);
                this.btnExitRoom.Visibility = System.Windows.Visibility.Visible;
                this.panelEnterRoom.Visibility = System.Windows.Visibility.Collapsed;
                this.TcpClientRoom = new TcpClientUtils(ServerName, this.currentRoomPort);
                this.localUser.Address = SystemIPAddress.ToString();
            }
        }

        public byte[] ReceiveTcpMessage(byte[] bytes, string endPoint)
        {
            if (null != bytes && bytes.Length > 0)
            {
                Protocol protocol = ProtocolFactory.CreateProtocol(bytes);
                protocol.Host = this;
                protocol.OnResponse();
            }
            return null;
        }

        public void CreateTcpListener(int port)
        {
            
        }

        private void btnExitRoom_Click(object sender, RoutedEventArgs e)
        {
            ExitRoomProtocol protocol = new ExitRoomProtocol();
            protocol.Data = txtRoomName.Text;

            this.TcpClientRoom.SendData(protocol);

            this.lblRoomInfo.Content = "";
            this.btnExitRoom.Visibility = System.Windows.Visibility.Collapsed;
            this.panelEnterRoom.Visibility = System.Windows.Visibility.Visible;
        }

        private void btnSendMessage_Click(object sender, RoutedEventArgs e)
        {
            string inputMessage = new TextRange(richInputMessage.Document.ContentStart, richInputMessage.Document.ContentEnd).Text;
            if (!string.IsNullOrWhiteSpace(inputMessage))
            {
                Protocol udpMessageProtocol = new ClientUdpMessageProtocol();
                udpMessageProtocol.Data = inputMessage;
                this.TcpClientRoom.SendData(udpMessageProtocol);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.txtRoomName.Focus();
        }
    }
}
