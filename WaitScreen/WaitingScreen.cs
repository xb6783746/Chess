using ClientAPI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WaitScreen
{
    [Screen]
    public class WaitScreen : UserControl, IWaitingScreen
    {

        public WaitScreen()
        {

            InitializeComponent();

            bitmap = new Bitmap(animateBox.Width, animateBox.Height);
            g = Graphics.FromImage(bitmap);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            center = new Point(bitmap.Width / 2, bitmap.Height / 2);
            animateBox.Image = bitmap;

        }

        private Label waitLabel;
        private PictureBox animateBox;
        private Bitmap bitmap;
        private Graphics g;
        private Timer timer1;
        private System.ComponentModel.IContainer components;

        private Button cancelButton;

        private float startAngle1 = 10, startAngle2 = 20;
        private Point center;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cancelButton = new System.Windows.Forms.Button();
            this.waitLabel = new System.Windows.Forms.Label();
            this.animateBox = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.animateBox)).BeginInit();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Location = new System.Drawing.Point(128, 197);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(157, 68);
            this.cancelButton.TabIndex = 0;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // waitLabel
            // 
            this.waitLabel.AutoSize = true;
            this.waitLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.waitLabel.Location = new System.Drawing.Point(128, 25);
            this.waitLabel.Name = "waitLabel";
            this.waitLabel.Size = new System.Drawing.Size(157, 31);
            this.waitLabel.TabIndex = 1;
            this.waitLabel.Text = "Подождите";
            // 
            // animateBox
            // 
            this.animateBox.Location = new System.Drawing.Point(144, 71);
            this.animateBox.Name = "animateBox";
            this.animateBox.Size = new System.Drawing.Size(120, 120);
            this.animateBox.TabIndex = 2;
            this.animateBox.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // WaitScreen
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.animateBox);
            this.Controls.Add(this.waitLabel);
            this.Controls.Add(this.cancelButton);
            this.Name = "WaitScreen";
            this.Size = new System.Drawing.Size(415, 295);
            ((System.ComponentModel.ISupportInitialize)(this.animateBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        public event Action Close;

        public UserControl GetScreen()
        {
            return this;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            g.Clear(Color.White);

            DrawCircle(g, 50, startAngle1);
            DrawCircle(g, 35, startAngle2);

            startAngle1 += 5;
            startAngle2 -= 5;

            animateBox.Image = bitmap;
            animateBox.Invalidate();
        }

        private void DrawCircle(Graphics g, int rad, float startAngle)
        {
            g.FillEllipse(Brushes.White, center.X - rad, center.Y - rad, 2 * rad, 2 * rad);

            for (int i = 0; i < 4; i++)
            {
                g.FillPie(Brushes.Black, center.X - rad, center.Y - rad, 2 * rad, 2 * rad, startAngle + 90*i, 45);
            }
        }
    }
}
