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
        private Label nickLabel;
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
            this.chatScreen = new ClientAPI.ChatScreen();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.window = new tmp.ModalWindow();
            this.nickLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // startRandomGameButton
            // 
            this.startRandomGameButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startRandomGameButton.Location = new System.Drawing.Point(322, 26);
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
            this.gameWithButton.Location = new System.Drawing.Point(322, 76);
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
            this.watchForButton.Location = new System.Drawing.Point(322, 126);
            this.watchForButton.Name = "watchForButton";
            this.watchForButton.Size = new System.Drawing.Size(127, 44);
            this.watchForButton.TabIndex = 6;
            this.watchForButton.Text = "Наблюдать за чужой игрой";
            this.watchForButton.UseVisualStyleBackColor = true;
            this.watchForButton.Click += new System.EventHandler(this.watchForButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.nickLabel);
            this.panel1.Controls.Add(this.chatScreen);
            this.panel1.Controls.Add(this.listBox1);
            this.panel1.Controls.Add(this.watchForButton);
            this.panel1.Controls.Add(this.gameWithButton);
            this.panel1.Controls.Add(this.startRandomGameButton);
            this.panel1.Location = new System.Drawing.Point(14, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(529, 486);
            this.panel1.TabIndex = 7;
            // 
            // chatScreen
            // 
            this.chatScreen.Location = new System.Drawing.Point(6, 257);
            this.chatScreen.Name = "chatScreen";
            this.chatScreen.Size = new System.Drawing.Size(301, 206);
            this.chatScreen.TabIndex = 8;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(6, 26);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(299, 225);
            this.listBox1.TabIndex = 7;
            // 
            // window
            // 
            this.window.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.window.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.window.Location = new System.Drawing.Point(50, 49);
            this.window.Name = "window";
            this.window.Size = new System.Drawing.Size(437, 377);
            this.window.TabIndex = 8;
            this.window.Visible = false;
            this.window.Load += new System.EventHandler(this.window_Load);
            // 
            // nickLabel
            // 
            this.nickLabel.AutoSize = true;
            this.nickLabel.Location = new System.Drawing.Point(3, 10);
            this.nickLabel.Name = "nickLabel";
            this.nickLabel.Size = new System.Drawing.Size(35, 13);
            this.nickLabel.TabIndex = 9;
            this.nickLabel.Text = "label1";
            // 
            // MainScreen
            // 
            this.Controls.Add(this.window);
            this.Controls.Add(this.panel1);          
            this.Name = "MainScreen";
            this.Size = new System.Drawing.Size(548, 501);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
            panel1.Visible = false;
        }
        private void watchForButton_Click(object sender, EventArgs e)
        {
            selectedEvent = WatchForGamer;

            window.Visible = true;
            panel1.Visible = false;
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

            panel1.Visible = true;
            window.Visible = false;
        }
        private void Cancel()
        {
            selectedEvent = null;

            panel1.Visible = true;
            window.Visible = false;
        }


        public string Nick
        {
            get { return nickLabel.Text; }
            set
            {
                chatScreen.Nick = value;
                IfInvoke(
                    new Action(
                        () => nickLabel.Text = value
                        )
                    );
            }
        }

        private void window_Load(object sender, EventArgs e)
        {

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
