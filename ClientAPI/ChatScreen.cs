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
using System.Text.RegularExpressions;

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
                {ChatMessageType.System, Brushes.Red},
                {ChatMessageType.Info, new SolidBrush(Color.FromArgb(60,100,100))}
            };

            selectBrush = new SolidBrush(Color.FromArgb(255, 250, 240));
            messCount = chatWindow.Height / chatWindow.ItemHeight;
            regex = new Regex(pattern);
            addAction = new Action<string>((x) =>
            {
                chatWindow.Items.Add(x);
                if (chatWindow.TopIndex >= chatWindow.Items.Count - messCount - 1)
                {
                    chatWindow.TopIndex = chatWindow.Items.Count - 1;
                }
            });

            chatWindow.DrawMode = DrawMode.OwnerDrawFixed;

            chatWindow.DrawItem += chatWindow_DrawItem;
        }

        private List<ChatMessage> messages;
        private object lck = new object();
        private Dictionary<ChatMessageType, Brush> brushes;
        private Regex regex;
        private const string pattern = @"^/(\w+) (\w+)";
        private int messCount;
        private Brush selectBrush;
        Action<string> addAction;

        private void chatWindow_DrawItem(object sender, DrawItemEventArgs e)
        {
           // e.DrawBackground();
            if(e.Index < 0)
            {
                return;
            }

            var tmp = messages[e.Index];
            Brush brush = brushes[tmp.Type];


            if (chatWindow.SelectedIndex == e.Index)
            {
                e.Graphics.FillRectangle(selectBrush, e.Bounds);
            }
            else
            {
                e.Graphics.FillRectangle(Brushes.White, e.Bounds);
            }

            e.Graphics.DrawString(
                chatWindow.Items[e.Index].ToString(),
                e.Font,
                brush,
                e.Bounds,
                StringFormat.GenericDefault
                );
        }

        public string Nick { get; set; }
        public void Receive(ChatMessage message)
        {
            lock (lck)
            {
                messages.Add(message);
                
                InvokeAdd(message.Message);
            }
        }  
        public event Action<ChatMessage> Send = (x) => { };

        //private void sendButton_Click(object sender, EventArgs e)
        //{
        //    if (messageBox.Text != "")
        //    {
        //        ChatMessage m, self;

        //        if (messageBox.Text[0] == '/')
        //        {
        //            messageBox.Text = messageBox.Text.Substring(1);
        //            string nick = messageBox.Text.Substring(0, messageBox.Text.IndexOf(' '));
        //            m = new ChatMessage("", nick, ChatMessageType.Private, messageBox.Text.Substring(nick.Length));
                   
        //            self = m.Copy();
        //            self.From = Nick + " -> " + nick;

        //        }
        //        else
        //        {
        //            m = new ChatMessage("", "", ChatMessageType.Public, messageBox.Text);     
           
        //            self = m.Copy();
        //            self.From = Nick;
                   
        //        }
        //        messageBox.Text = "";

        //        Receive(self);

        //        Send(m);
               
        //    }
        //}
        private void sendButton_Click(object sender, EventArgs e)
        {
            if (messageBox.Text != "")
            {
                ChatMessage m, self;

                var privateMessage = regex.Match(messageBox.Text);
                if (privateMessage.Success)
                {
                    m = new ChatMessage(
                        Nick, 
                        privateMessage.Groups[1].Value, 
                        ChatMessageType.Private, 
                        privateMessage.Groups[2].Value
                        );

                    self = m.Copy();
                    self.From = Nick + " -> " + m.To;
                }
                else
                {
                    m = new ChatMessage(Nick, "", ChatMessageType.Public, messageBox.Text);

                    self = m.Copy();
                }

                messageBox.Text = "";

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
                chatWindow.Invoke(addAction, text);
            }
            else
            {
                addAction(text);
            }
        }

        private void chatWindow_SelectedIndexChanged(object sender, EventArgs e)
        {
            chatWindow.Invalidate();

            messageBox.Text = "/" + messages[chatWindow.SelectedIndex].From + " ";
            messageBox.Focus();
            messageBox.SelectionStart = messageBox.Text.Length;
        }
    }
}
