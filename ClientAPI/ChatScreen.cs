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
using Network;

namespace ClientAPI
{
    public partial class ChatScreen : UserControl, IMessenger
    {
        public ChatScreen()
        {
            InitializeComponent();

            messages = new List<ChatMessage>();
            brushes = new Dictionary<ChatMessageType, Brush>()
            {
                {ChatMessageType.Private, Brushes.Purple},
                {ChatMessageType.Public, Brushes.Black},
                {ChatMessageType.System, Brushes.Red}
            };

            selectBrush = new SolidBrush(Color.FromArgb(255, 250, 240));

            chatWindow.DrawMode = DrawMode.OwnerDrawFixed;

            chatWindow.DrawItem += chatWindow_DrawItem;
        }

        private List<ChatMessage> messages;
        private object lck = new object();
        Dictionary<ChatMessageType, Brush> brushes;

        private Brush selectBrush;

        void chatWindow_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            var tmp = messages[e.Index];
            Brush brush = brushes[tmp.Type];


            if (chatWindow.SelectedIndex == e.Index)
            {
                e.Graphics.FillRectangle(selectBrush, e.Bounds);
            }

            e.Graphics.DrawString(
                chatWindow.Items[e.Index].ToString(),
                e.Font,
                brush,
                e.Bounds,
                StringFormat.GenericDefault
                );
        }

        public void Receive(ChatMessage message)
        {
            lock (lck)
            {
                messages.Add(message);
                
                InvokeAdd(message.Message);
            }
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            if (messageBox.Text != "")
            {
                ChatMessage m, self;

                if (messageBox.Text[0] == '/')
                {
                    messageBox.Text = messageBox.Text.Substring(1);
                    string nick = messageBox.Text.Substring(0, messageBox.Text.IndexOf(' '));
                    m = new ChatMessage("", nick, ChatMessageType.Private, messageBox.Text.Substring(nick.Length));
                   
                    self = m.Copy();
                    self.From = "Вы -> " + nick;

                }
                else
                {
                    m = new ChatMessage("", "", ChatMessageType.Public, messageBox.Text);     
           
                    self = m.Copy();
                    self.From = "Вы";

                    
                }

                Receive(self);

                Send(m);
               
            }
        }

        private void messageBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                sendButton.PerformClick();
            }
        }

        private void InvokeAdd(string text)
        {
            if (chatWindow.InvokeRequired)
            {
                chatWindow.Invoke( new Action( () => chatWindow.Items.Add(text)) );
            }
            else
            {
                chatWindow.Items.Add(text);
            }
        }


        public event Action<ChatMessage> Send = (x) => { };
    }
}
