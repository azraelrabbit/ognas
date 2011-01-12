using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Ognas.Lib;
using Ognas.Lib.Shoguns;

namespace TestApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private int iSunshangxiang = 0;
        private int iCount = 0;

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

        private void button3_Click(object sender, EventArgs e)
        {
            iCount++;
            ShogunCenter sc = new ShogunCenter();
            List<Shogun> list = sc.GetSubShogunList(8);

            if (list.Find(delegate(Shogun s) { return s.Code == ShogunCode.SunShangXiang; }) != null)
            {
                iSunshangxiang++;
            }

            this.listBox1.DataSource = list;
            this.listBox1.DisplayMember = "Name";

            double d = (double)iSunshangxiang / (double)iCount;

            this.label1.Text = (d * 100).ToString();
        }
    }
}
