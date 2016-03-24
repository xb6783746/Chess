using ChessClient.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessClient
{
    public partial class MainForm : Form, IMainForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private UserControl control;

        public UserControl Screen
        {
            get
            {
               return control;
            }
            set
            {
                Controls.Remove(control);

                control = value;
                control.Location = new Point(10, 10);
                flowLayoutPanel1.Controls.Add(control);

            }
        }
    }
}
