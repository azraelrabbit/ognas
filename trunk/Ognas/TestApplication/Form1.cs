using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ognas.Lib.Shoguns;
using Ognas.Lib;

namespace TestApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShogunCenter sc = new ShogunCenter();
            List<Shogun> list = sc.GetSubShogunList(TypeofInitialShogunList.ForMaster);

            this.listBox1.DataSource = list;
            this.listBox1.DisplayMember = "Name";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ShogunCenter sc = new ShogunCenter();
            List<Shogun> list = sc.GetSubShogunList(TypeofInitialShogunList.ForPlayer);

            this.listBox1.DataSource = list;
            this.listBox1.DisplayMember = "Name";
        }
    }
}
