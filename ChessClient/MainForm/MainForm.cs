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

            clearAction = () => flowLayoutPanel1.Controls.Clear();
            addAction = () => flowLayoutPanel1.Controls.Add(control);
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

                IfInvoke(flowLayoutPanel1, clearAction);

                control = value;

                IfInvoke(flowLayoutPanel1, addAction);

            }
        }

        private void IfInvoke(FlowLayoutPanel control, Action action)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }

        
    }
}
