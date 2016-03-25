using ClientAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainScreen
{
    public class MainScreen :UserControl, IMainScreen
    {
        public MainScreen()
        {
            InitializeComponent();
        }

        private TextBox chatWindow;
        private TextBox messageBox;
        private Button sendButton;
    
        public bool Challenge(string from)
        {
            throw new NotImplementedException();
        }

        public void Receive(string message)
        {
            chatWindow.Text += message + Environment.NewLine;
        }

        public event Action RandomGame;
        public event Action<string> GameWith;
        public event Action<string> WatchForGamer;
        public event Action<string> ChangeNick;
        public event Action<string> Send;

        public UserControl GetScreen()
        {
            return this;
        }

        private void InitializeComponent()
        {
            this.chatWindow = new System.Windows.Forms.TextBox();
            this.messageBox = new System.Windows.Forms.TextBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chatWindow
            // 
            this.chatWindow.Location = new System.Drawing.Point(4, 159);
            this.chatWindow.Multiline = true;
            this.chatWindow.Name = "chatWindow";
            this.chatWindow.ReadOnly = true;
            this.chatWindow.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.chatWindow.Size = new System.Drawing.Size(214, 108);
            this.chatWindow.TabIndex = 0;
            // 
            // messageBox
            // 
            this.messageBox.Location = new System.Drawing.Point(4, 273);
            this.messageBox.Name = "messageBox";
            this.messageBox.Size = new System.Drawing.Size(214, 20);
            this.messageBox.TabIndex = 1;
            this.messageBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.messageBox_KeyDown);
            // 
            // sendButton
            // 
            this.sendButton.Location = new System.Drawing.Point(224, 272);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(73, 21);
            this.sendButton.TabIndex = 2;
            this.sendButton.Text = "Отправить";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // MainScreen
            // 
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.messageBox);
            this.Controls.Add(this.chatWindow);
            this.Name = "MainScreen";
            this.Size = new System.Drawing.Size(482, 302);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void sendButton_Click(object sender, EventArgs e)
        {
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
    }
}
