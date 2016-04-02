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

            clearAction = () => Controls.Clear();
            addAction = () =>
            {
                this.Width = control.Width;
                this.Height = control.Height + 20;

                //Position(control);

                Controls.Add(control);
                
            };
        }

        private UserControl control;
        private Action clearAction;
        private Action addAction;

        public UserControl Screen
        {
            get
            {
                return control;
            }
            set
            {

                IfInvoke(clearAction);

                control = value;
                if (control != null)
                {
                    IfInvoke(addAction);
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
        private void Position(Control control)
        {
            control.Location = new Point(
                Width/2 - control.Width/2,
                Height / 2 - control.Height / 2
                );
        }
        
    }
}
