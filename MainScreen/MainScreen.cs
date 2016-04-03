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

namespace MainScreen.NewFolder1
{
    public partial class MainScreen : UserControl, IMainScreen
    {
        public MainScreen()
        {
            InitializeComponent();

            chatScreen.Send += (x) => Send(x);

            window.Select += UserSelect;
            window.Cancel += Cancel;
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
        public UserControl GetScreen()
        {
            return this;
        }
        public void Enable()
        {

        }
        public void Disable()
        {

        }

        public event Action RandomGame = () => { };
        public event Action<string> GameWith = (x) => { };
        public event Action<string> WatchForGamer = (x) => { };
        public event Action<string> ChangeNick = (x) => { };
        public event Action<ChatMessage> Send = (x) => { };

        private event Action<string> selectedEvent;

        private void gameWithButton_Click(object sender, EventArgs e)
        {
            selectedEvent = GameWith;

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

    }
}
