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
using Ognas.Lib;
using Ognas.Lib.Protocols;
using Ognas.Lib.Shoguns;
using Ognas.Resource;
using System.Windows.Media.Animation;

namespace Ognas.Client
{
    /// <summary>
    /// ShogunSelectWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ShogunSelectWindow : Window
    {

        SelectionShogunProtocol sgp;
        double OldWidth;
        double OldHeight;
        bool HasSelected = false;
        Image Selected;
        /// <summary>
        /// selected shogun
        /// </summary>
        public Shogun sgs
        {
            get;
            set;
        }
        public ShogunSelectWindow(object shogunProtocol)
        {
            this.sgp = (SelectionShogunProtocol)shogunProtocol;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (Shogun sg in sgp.shoGunList)
            {
                string name = sg.Name;
                string pic = sg.Picture;
                this.lboxShogun.Items.Add(sg);
            }
            lboxShogun.DisplayMemberPath = "Name";

            DisplayPic2(sgp.shoGunList);
        }
        public void DisplayPic2(List<Shogun> list)
        {
            stkpShogun.Children.Clear();

            foreach (Shogun sg in list)
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                System.Drawing.Bitmap bmp = (System.Drawing.Bitmap)Images_Shogun.ResourceManager.GetObject(sg.Picture);
                bmp.Save(ms, bmp.RawFormat);
                BitmapImage bmg = new BitmapImage();// (BitmapImage)Images_Shogun.ResourceManager.GetStream(s.Picture);
                bmg.BeginInit();
                ms.Position = 0;
                bmg.StreamSource = ms;
                // bmg.UriSource = new Uri(@"Ognas.Resource.Images_Shogun/" + s.Picture, UriKind.RelativeOrAbsolute);
                // bmg.DecodePixelHeight = 150;

                bmg.EndInit();
                // var xaml = "<Image  Width=\"" + bmg.Width + "\" Height=\"" + bmg.Height + "\" Visibility=\"Collapsed\" />";

                Image image = new Image();
                image.BeginInit();
                image.Name = sg.Name;
                image.Source = bmg;
                image.Height = 150;
                image.Tag = sg;
                image.MouseLeftButtonUp += new MouseButtonEventHandler(image_MouseLeftButtonUp);
                image.MouseEnter += new MouseEventHandler(image_MouseEnter);
                image.MouseLeave += new MouseEventHandler(image_MouseLeave);
                image.EndInit();

                tooltip tlp = new tooltip();
                tlp.ShogunName = sg.Name;
                tlp.description = sg.Name;
                tlp.setVam();

                ToolTipService.SetInitialShowDelay(image, 2000);
                ToolTipService.SetBetweenShowDelay(image, 2000);
                ToolTipService.SetShowDuration(image, 7000);

                ToolTipService.SetToolTip(image, tlp);

                stkpShogun.Children.Add(image);

            }
        }

        void image_MouseLeave(object sender, MouseEventArgs e)
        {
            Image i = ((Image)e.Source);
            //i.Height = OldHeight;
            //i.Width = OldWidth;
            if (HasSelected)
            {
                if (i == this.Selected)
                {
                    return;
                }
            }
            var widthAnimation1 = new DoubleAnimation();
            widthAnimation1.From = i.ActualWidth;
            widthAnimation1.To = OldWidth;
            widthAnimation1.Duration = TimeSpan.FromMilliseconds(200);
            i.BeginAnimation(Image.WidthProperty, widthAnimation1);

            var heightAnimation1 = new DoubleAnimation();
            heightAnimation1.From = i.ActualHeight;
            heightAnimation1.To = OldHeight;
            heightAnimation1.Duration = TimeSpan.FromMilliseconds(200);
            i.BeginAnimation(Image.HeightProperty, heightAnimation1);
        }

        void image_MouseEnter(object sender, MouseEventArgs e)
        {

            Image i = ((Image)e.Source);
            if (HasSelected)
            {
                if (i == this.Selected)
                {
                    return;
                }
            }

            OldHeight = i.ActualHeight;
            OldWidth = i.ActualWidth;
            DoubleAnimation widthAnimation = new DoubleAnimation();
            widthAnimation.From = i.ActualWidth;
            widthAnimation.To = i.ActualWidth * 1.2;
            widthAnimation.Duration = TimeSpan.FromMilliseconds(150);
            i.BeginAnimation(Image.WidthProperty, widthAnimation);

            DoubleAnimation heightAnimation = new DoubleAnimation();
            heightAnimation.From = i.ActualHeight;
            heightAnimation.To = i.ActualHeight * 1.2;
            heightAnimation.Duration = TimeSpan.FromMilliseconds(150);
            i.BeginAnimation(Image.HeightProperty, heightAnimation);



            //Storyboard storyImage = new Storyboard();

            //Image i = ((Image)e.Source);
            //OldHeight = i.ActualHeight;
            //OldWidth = i.ActualWidth;
            //i.Height = i.ActualHeight * 1.2;
            //i.Width = i.ActualWidth * 1.2;


            // 动画结束
        }

        void image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (HasSelected)
            {
                var widthAnimation1 = new DoubleAnimation();
                widthAnimation1.From = Selected.ActualWidth;
                widthAnimation1.To = OldWidth;
                widthAnimation1.Duration = TimeSpan.FromMilliseconds(200);
                this.Selected.BeginAnimation(Image.WidthProperty, widthAnimation1);

                var heightAnimation1 = new DoubleAnimation();
                heightAnimation1.From = Selected.ActualHeight;
                heightAnimation1.To = OldHeight;
                heightAnimation1.Duration = TimeSpan.FromMilliseconds(200);
                Selected.BeginAnimation(Image.HeightProperty, heightAnimation1);
            }
            this.Selected = (Image)e.Source;
            this.HasSelected = true;
            string name = ((Image)e.Source).Name;
            foreach (object o in lboxShogun.Items)
            {
                if (((Shogun)o).Name == name)
                {
                    lboxShogun.SelectedItem = o;
                }
            }
        }
        public void DisplayPic(List<Shogun> list)
        {
            stkpShogun.Children.Clear();
            //  canvas1.Children.Clear();

            int left = 5;
            int intI = 1;
            foreach (Shogun s in list)
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                System.Drawing.Bitmap bmp = (System.Drawing.Bitmap)Images_Shogun.ResourceManager.GetObject(s.Picture);
                bmp.Save(ms, bmp.RawFormat);
                BitmapImage bmg = new BitmapImage();// (BitmapImage)Images_Shogun.ResourceManager.GetStream(s.Picture);
                bmg.BeginInit();
                ms.Position = 0;
                bmg.StreamSource = ms;
                // bmg.UriSource = new Uri(@"Ognas.Resource.Images_Shogun/" + s.Picture, UriKind.RelativeOrAbsolute);
                bmg.DecodePixelHeight = 150;

                bmg.EndInit();

                FormatConvertedBitmap fcb = new FormatConvertedBitmap();
                fcb.BeginInit();
                fcb.Source = bmg;
                fcb.EndInit();

                Image img = new Image();
                switch (intI)
                {
                    case 1:
                        img = this.image1;
                        break;
                    case 2:
                        img = this.image2;
                        break;
                    case 3:
                        img = this.image3;
                        break;
                    case 4:
                        img = this.image4;
                        break;
                    case 5:
                        img = this.image5;
                        break;

                }
                img.BeginInit();
                // img.Name = s.Name;
                //img.Height = bmg.Height;

                //img.Width = bmg.Width;
                img.Tag = s;
                img.Source = fcb;
                Thickness margin = new Thickness();
                margin.Top = 5;
                margin.Left = left;
                img.Margin = margin;
                // img.MouseLeftButtonUp += new MouseButtonEventHandler(img_MouseLeftButtonUp);
                img.EndInit();

                //stkpShogun.Children.Add(img);
                left += int.Parse(bmg.Width.ToString()) + 5;
                ms.Close();
                ms.Dispose();
                intI++;
                //canvas1.Children.Add(img);
            }

        }

        void img_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Image im = (Image)e.Source;
            foreach (object lt in lboxShogun.Items)
            {
                if (((Shogun)lt) == ((Shogun)im.Tag))
                {
                    lboxShogun.SelectedItem = lt;
                }
            }
        }

        private void ShogunSelected_Click(object sender, RoutedEventArgs e)
        {
            sgs = (Shogun)lboxShogun.SelectedItem;
            this.DialogResult = true;
        }


    }
}
