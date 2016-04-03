using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using ClientAPI;

namespace WaitScreen
{
    [Screen]
    public partial class WaitingScreen : UserControl, IWaitingScreen
    {
        public WaitingScreen()
        {
            InitializeComponent();

            timer = new System.Threading.Timer((x) => Tick());

            bitmap = new Bitmap(animateBox.Width, animateBox.Height);
            g = Graphics.FromImage(bitmap);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            center = new Point(bitmap.Width / 2, bitmap.Height / 2);
            animateBox.Image = bitmap;
        }

        private Bitmap bitmap;
        private Graphics g;
        private System.Threading.Timer timer;
        private float startAngle1 = 10, startAngle2 = 20;
        private Point center;

        public UserControl GetScreen()
        {
            return this;
        }
        public void Enable()
        {
            timer.Change(0, 100);
        }
        public void Disable()
        {
            timer.Change(Timeout.Infinite, Timeout.Infinite);
        }
        public event Action Close = () => { };

        private void Tick()
        {
            g.Clear(Color.White);

            DrawCircle(g, 50, startAngle1);
            DrawCircle(g, 35, startAngle2);

            startAngle1 += 5;
            startAngle2 -= 5;

            //animateBox.Image = bitmap;
            animateBox.Invalidate();
        }
        private void cancelClick(object sender, EventArgs e)
        {
            //Close();
        }
        private void DrawCircle(Graphics g, int rad, float startAngle)
        {
            g.FillEllipse(Brushes.White, center.X - rad, center.Y - rad, 2 * rad, 2 * rad);

            for (int i = 0; i < 4; i++)
            {
                g.FillPie(
                    Brushes.Black, 
                    center.X - rad, 
                    center.Y - rad, 
                    2 * rad, 
                    2 * rad, 
                    startAngle + 90*i, 
                    45);
            }
        }
    }
}
