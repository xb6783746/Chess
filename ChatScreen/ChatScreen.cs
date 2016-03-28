using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientAPI;

namespace ChatScreen
{
    public partial class ChatScreen : UserControl, IMessenger
    {
        public ChatScreen()
        {
            InitializeComponent();
        }

        public void Receive(string message)
        {
            chatWindow.Text += message + Environment.NewLine;
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            if (messageBox.Text != "")
            {
                Send(messageBox.Text);
            }
        }

        private void messageBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                sendButton.PerformClick();
            }
        }


        public event Action<string> Send;
    }
}
