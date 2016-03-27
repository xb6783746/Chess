using ClientAPI;
using System;
using System.Collections.Generic;
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

            window = new ModalWindow();
            window.Location = new System.Drawing.Point
                (
                    this.Width / 2 - window.Width / 2,
                    this.Height / 2 - window.Height / 2
                );
        }

        private TextBox chatWindow;
        private TextBox messageBox;
        private Button startRandomGameButton;
        private Button gameWithButton;
        private Button watchForButton;
        private Button sendButton;
        private Panel panel1;
        private ListBox listBox1;
        private ModalWindow window;

        public bool Challenge(string from)
        {
            return
                MessageBox.Show(
                "Игрок " + from + " приглашает вас в игру", 
                "", 
                MessageBoxButtons.OKCancel) == DialogResult.OK;

        }

        public void Receive(string message)
        {
            chatWindow.Text += message + Environment.NewLine;
        }

        public event Action RandomGame = () => { };
        public event Action<string> GameWith = (x) => { };
        public event Action<string> WatchForGamer = (x) => { };
        public event Action<string> ChangeNick = (x) => { };
        public event Action<string> Send = (x) => { };

        public UserControl GetScreen()
        {
            return this;
        }

        private void InitializeComponent()
        {
            this.chatWindow = new System.Windows.Forms.TextBox();
            this.messageBox = new System.Windows.Forms.TextBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.startRandomGameButton = new System.Windows.Forms.Button();
            this.gameWithButton = new System.Windows.Forms.Button();
            this.watchForButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chatWindow
            // 
            this.chatWindow.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.chatWindow.Location = new System.Drawing.Point(16, 264);
            this.chatWindow.Multiline = true;
            this.chatWindow.Name = "chatWindow";
            this.chatWindow.ReadOnly = true;
            this.chatWindow.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.chatWindow.Size = new System.Drawing.Size(331, 158);
            this.chatWindow.TabIndex = 0;
            // 
            // messageBox
            // 
            this.messageBox.Location = new System.Drawing.Point(16, 427);
            this.messageBox.Name = "messageBox";
            this.messageBox.Size = new System.Drawing.Size(331, 20);
            this.messageBox.TabIndex = 1;
            this.messageBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.messageBox_KeyDown);
            // 
            // sendButton
            // 
            this.sendButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sendButton.Location = new System.Drawing.Point(353, 427);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(73, 21);
            this.sendButton.TabIndex = 2;
            this.sendButton.Text = "Отправить";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
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
            this.panel1.Controls.Add(this.listBox1);
            this.panel1.Controls.Add(this.watchForButton);
            this.panel1.Controls.Add(this.chatWindow);
            this.panel1.Controls.Add(this.gameWithButton);
            this.panel1.Controls.Add(this.messageBox);
            this.panel1.Controls.Add(this.sendButton);
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
            this.listBox1.Size = new System.Drawing.Size(331, 238);
            this.listBox1.TabIndex = 7;
            // 
            // MainScreen1
            // 
            this.Controls.Add(this.panel1);
            this.Name = "MainScreen1";
            this.Size = new System.Drawing.Size(517, 468);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            Receive(messageBox.Text);

            Send(messageBox.Text);

            messageBox.Text = "";
        }

        private void messageBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                sendButton.PerformClick();
            }
        }

        private void gameWithButton_Click(object sender, EventArgs e)
        {
            Task.Run(
                () =>
                {
                    string user = GetUser();

                    if (user != "")
                    {
                        GameWith(user);
                    }
                });
        }

        private void ShowWindow()
        {
                 
            Invoke(new Action(() =>
                {
                    panel1.Enabled = false;
                    Controls.Add(window);
                    Controls.SetChildIndex(window, 0);
                }));
        }
        private void CloseWindow()
        {
            Invoke(new Action(() =>
           {
               Controls.Remove(window);
               panel1.Enabled = true;
           }));
        }

        private string GetUser()
        {
            ShowWindow();
            window.WaitHandle.WaitOne();

            CloseWindow();

            return window.Result;
        }

        private void watchForButton_Click(object sender, EventArgs e)
        {
            Task.Run(
               () =>
               {
                   string user = GetUser();

                   if (user != "")
                   {
                       WatchForGamer(user);
                   }
               });
        }

        private void startRandomGameButton_Click(object sender, EventArgs e)
        {
            RandomGame();
        }
    }
}
