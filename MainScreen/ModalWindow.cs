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

namespace tmp
{
    public partial class ModalWindow : UserControl
    {
        public ModalWindow()
        {
            InitializeComponent();

            WaitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
        }

        public EventWaitHandle WaitHandle
        {
            get; private set;
        }
        public string Result { get; private set; }

        public void Activate(List<string> users)
        {
            Result = "";

            userBox.Items.Clear();

            foreach(var item in users)
            {
                userBox.Items.Add(item);      
            }

        }


        private void acceptButton_Click(object sender, EventArgs e)
        {
            Result = inputBox.Text;

            WaitHandle.Set();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Result = "";

            WaitHandle.Set();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void userBox_SelectedValueChanged(object sender, EventArgs e)
        {
            inputBox.Text = userBox.SelectedItem as string;
        }
    }
}
