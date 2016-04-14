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

                IfInvoke(() =>
                {
                    Controls.Clear();
                });

                control = value;

                if (control != null)
                {
                    IfInvoke(() =>
                    {
                        this.Width = control.Width;
                        this.Height = control.Height + 20;

                        Controls.Add(control);
                    });
                }

            }
        }

        private void IfInvoke(Action action)
        {
            if (InvokeRequired)
            {
                Invoke(action);
            }
            else
            {
                action();
            }
        }
        
    }
}
