using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Network;
using ClientAPI;

namespace MainScreen
{
    public partial class MainWindow : UserControl, IMainScreen
    {
        public MainWindow()
        {
            InitializeComponent();

            this.window = new MainScreen.ModalWindow();
            this.window.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.window.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.window.Location = new Point(25, 26);
            this.window.Name = "window";
            this.window.Size = new System.Drawing.Size(468, 420);
            this.window.TabIndex = 0;
            window.Visible = false;

            Controls.Add(window);

            chatScreen.Send += (x) => Send(x);

            window.Select += UserSelect;
            window.Cancel += Cancel;
        }

        private ModalWindow window;
        private string[] algos;
        private string[] users;

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
        public bool Challenge(string from)
        {
            return
                MessageBox.Show(
                "Игрок " + from + " приглашает вас в игру",
                "",
                MessageBoxButtons.YesNo) == DialogResult.Yes;

        }
        public void Receive(ChatMessage message)
        {
            chatScreen.Receive(message);
        }
        public UserControl GetScreen()
        {
            return this;
        }
        public void Enable()
        {
            //GelAlgos();
        }
        public void Disable()
        {

        }

        public event Action RandomGame = () => { };
        public event Action<string> GameWith = (x) => { };
        public event Action<string> GameWithComputer = (x) => { };
        public event Action<string> WatchForGamer = (x) => { }; 
        public event Action<string> ChangeNick = (x) => { };
        public event Action<ChatMessage> Send = (x) => { };
        public event Action GetOnline = () => { };
        //public event Action GetAlgos = () => { };

        private event Action<string> selectedEvent;

        private void gameWithButton_Click(object sender, EventArgs e)
        {
            selectedEvent = GameWith;
            window.Activate(users);

            window.Visible = true;
            mainPanel.Visible = false;
        }
        private void gameWithComputerButton_Click(object sender, EventArgs e)
        {
            selectedEvent = GameWithComputer;
            window.Activate(algos);

            window.Visible = true;
            mainPanel.Visible = false;
        }
        private void watchForButton_Click(object sender, EventArgs e)
        {
            selectedEvent = WatchForGamer;

            window.Visible = true;
            mainPanel.Visible = false;
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

            mainPanel.Visible = true;
            window.Visible = false;
        }
        private void Cancel()
        {
            selectedEvent = null;

            mainPanel.Visible = true;
            window.Visible = false;
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

        public void SetOnlineList(string[] online)
        {
            users = online.Where((x) => x != Nick).ToArray();
            Action q = () => 
            {
                usersListBox.Items.Clear();
                for (int i = 0; i < users.Length; i++)
                {
                    usersListBox.Items.Add(users[i]);
                }
              
            };

            IfInvoke(q);
        }

        private void UpdateUsers_Click(object sender, EventArgs e)
        {
            GetOnline();
        }


        public void SetAlgoList(string[] algos)
        {
            this.algos = algos;
        }


        public event Action GelAlgos = () => { };
    }
}
