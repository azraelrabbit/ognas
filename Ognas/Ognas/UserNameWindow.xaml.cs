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
using System.Windows.Shapes;
using System.ComponentModel;
using SocketUtils;
using Platform.CommonUtils;
using Ognas.Client.Protocols;
using Ognas.Lib.Protocols;

namespace Ognas.Client
{
    /// <summary>
    /// Interaction logic for UserNameWindow.xaml
    /// </summary>
    public partial class UserNameWindow : Window
    {
        public UserNameWindow()
        {
            InitializeComponent();
        }

        public string ResponseText
        {
            get { return ResponseTextBox.Text; }
            set { ResponseTextBox.Text = value; }
        }

        public bool ValidateUserName()
        {
            if (string.IsNullOrWhiteSpace(ResponseText))
            {
                lblError.Content = "Please enter your user name";
                return false;
            }

            Protocol protocol = new ClientRegisterUserProtocol();
            protocol.Data = ResponseText;
            var bytes = MainWindow.TcpClientSystem.SendData(protocol);
            if (!BitConverter.ToBoolean(bytes, 0))
            {
                lblError.Content = "An error occurred.";
                return false;
            }
            return true;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (this.ValidateUserName())
            {
                this.DialogResult = true;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.ResponseTextBox.Focus();
        }
    }
}
