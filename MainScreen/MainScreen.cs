using ClientAPI;
using Network;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using tmp;

namespace MainScreen
{
    public class MainScreen : UserControl, IMainScreen
    {
        public MainScreen()
        {
            InitializeComponent();

            window.Select += UserSelect;
            window.Cancel += Cancel;

            chatScreen.Send += (x) => Send(x);
        }
        private Button startRandomGameButton;
        private Button gameWithButton;
        private Button watchForButton;
        private Panel panel1;
        private ListBox listBox1;
        private ChatScreen chatScreen;
        private ModalWindow window;

        public bool Challenge(string from)
        {
            return
                MessageBox.Show(
                "Игрок " + from + " приглашает вас в игру", 
                "", 
                MessageBoxButtons.OKCancel) == DialogResult.OK;

        }

        public void Receive(ChatMessage message)
        {
            chatScreen.Receive(message);
        }

        public event Action RandomGame = () => { };
        public event Action<string> GameWith = (x) => { };
        public event Action<string> WatchForGamer = (x) => { };
        public event Action<string> ChangeNick = (x) => { };
        public event Action<ChatMessage> Send = (x) => { };

        private event Action<string> selectedEvent;

        public UserControl GetScreen()
        {
            return this;
        }

        private void InitializeComponent()
        {
            this.startRandomGameButton = new System.Windows.Forms.Button();
            this.gameWithButton = new System.Windows.Forms.Button();
            this.watchForButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.window = new tmp.ModalWindow();
            this.chatScreen = new ClientAPI.ChatScreen();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // startRandomGameButton
            // 
            this.startRandomGameButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startRandomGameButton.Location = new System.Drawing.Point(362, 179);
            this.startRandomGameButton.Name = "startRandomGameButton";
            this.startRandomGameButton.Size = new System.Drawing.Size(127, 44);
            this.startRandomGameButton.TabIndex = 3;
            this.startRandomGameButton.Text = "Случайная игра";
            this.startRandomGameButton.UseVisualStyleBackColor = true;
            this.startRandomGameButton.Click += new System.EventHandler(this.startRandomGameButton_Click);
            // 
            // gameWithButton
            // 
            this.gameWithButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gameWithButton.Location = new System.Drawing.Point(362, 229);
            this.gameWithButton.Name = "gameWithButton";
            this.gameWithButton.Size = new System.Drawing.Size(127, 44);
            this.gameWithButton.TabIndex = 5;
            this.gameWithButton.Text = "Играть с другом";
            this.gameWithButton.UseVisualStyleBackColor = true;
            this.gameWithButton.Click += new System.EventHandler(this.gameWithButton_Click);
            // 
            // watchForButton
            // 
            this.watchForButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.watchForButton.Location = new System.Drawing.Point(362, 279);
            this.watchForButton.Name = "watchForButton";
            this.watchForButton.Size = new System.Drawing.Size(127, 44);
            this.watchForButton.TabIndex = 6;
            this.watchForButton.Text = "Наблюдать за чужой игрой";
            this.watchForButton.UseVisualStyleBackColor = true;
            this.watchForButton.Click += new System.EventHandler(this.watchForButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chatScreen);
            this.panel1.Controls.Add(this.listBox1);
            this.panel1.Controls.Add(this.watchForButton);
            this.panel1.Controls.Add(this.gameWithButton);
            this.panel1.Controls.Add(this.startRandomGameButton);
            this.panel1.Location = new System.Drawing.Point(0, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(503, 456);
            this.panel1.TabIndex = 7;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(16, 13);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(299, 186);
            this.listBox1.TabIndex = 7;
            // 
            // window
            // 
            this.window.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.window.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.window.Location = new System.Drawing.Point(26, 27);
            this.window.Name = "window";
            this.window.Size = new System.Drawing.Size(437, 377);
            this.window.TabIndex = 8;
            this.window.Visible = false;
            // 
            // chatScreen1
            // 
            this.chatScreen.Location = new System.Drawing.Point(16, 205);
            this.chatScreen.Name = "chatScreen1";
            this.chatScreen.Size = new System.Drawing.Size(299, 248);
            this.chatScreen.TabIndex = 8;
            // 
            // MainScreen
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.window);
            this.Name = "MainScreen";
            this.Size = new System.Drawing.Size(517, 468);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        //private void sendButton_Click(object sender, EventArgs e)
        //{
        //    ChatMessage mesg;

        //    if (messageBox.Text[0] == '/')
        //    {
        //        messageBox.Text = messageBox.Text.Substring(1);

        //        string to = messageBox.Text.Split(' ')[0];
        //        string text = messageBox.Text.Substring(to.Length + 1);

        //        mesg = new ChatMessage(Nick, to, ChatMessageType.Private, text);              
        //    }
        //    else
        //    {
        //        mesg = new ChatMessage(Nick, "", ChatMessageType.Public, messageBox.Text);
        //    }

        //    Send(mesg);

        //    mesg.From = "Вы";
        //    Receive(mesg);
        //}

        //private void messageBox_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        sendButton.PerformClick();
        //    }
        //}

        private void gameWithButton_Click(object sender, EventArgs e)
        {
            selectedEvent = GameWith;

            window.Visible = true;
            panel1.Enabled = false;
        }
        private void watchForButton_Click(object sender, EventArgs e)
        {
            selectedEvent = WatchForGamer;

            window.Visible = true;
            panel1.Enabled = false;
        }
        private void startRandomGameButton_Click(object sender, EventArgs e)
        {
            RandomGame();
        }

        private void UserSelect(string user)
        {
            if (selectedEvent != null)
            {
                selectedEvent(user);
            }

            selectedEvent = null;
            panel1.Enabled = true;
        }
        private void Cancel()
        {
            selectedEvent = null;

            panel1.Enabled = true;
        }


        public string Nick
        {
            get;
            set;
        }
    }
}
