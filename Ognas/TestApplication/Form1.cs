using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Ognas.Lib;
using Ognas.Lib.Shoguns;
using System.Drawing;

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

            DisplayPic(list);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ShogunCenter sc = new ShogunCenter();
            List<Shogun> list = sc.GetSubShogunList(TypeofInitialShogunList.ForPlayer);

            this.listBox1.DataSource = list;
            this.listBox1.DisplayMember = "Name";

            DisplayPic(list);
        }

        public void DisplayPic(List<Shogun> list)
        {
            int locX = 0;

            panel1.Controls.Clear();

            foreach (Shogun s in list)
            {
                Bitmap bmg = (Bitmap)Ognas.Resource.Images_Shogun.ResourceManager.GetObject(s.Picture);
                Image img = bmg.GetThumbnailImage(bmg.Width / 2, bmg.Height / 2,
                    null, IntPtr.Zero);
                bmg.Dispose();

                PictureBox pb = new PictureBox();
                pb.Location = new Point(locX, 0);
                locX += img.Width + 10;
                pb.Image = img;
                pb.SizeMode = PictureBoxSizeMode.AutoSize;
                panel1.Controls.Add(pb);
            }
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

            DisplayPic(list);
        }
    }
}
