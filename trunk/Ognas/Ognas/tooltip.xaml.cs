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

namespace Ognas.Client
{
    /// <summary>
    /// tooltip.xaml 的交互逻辑
    /// </summary>
    public partial class tooltip : UserControl
    {
        public tooltip()
        {
            InitializeComponent();
        }

        public string ShogunName
        {

            get;
            set;
        }

        public string description
        {

            get;
            set;
        }
        public void setVam()
        {
            this.shogunName.Content = this.ShogunName;
            this.Description.Text = this.description;
        }
    }
}
