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

namespace LogInScreen
{
    public partial class LoginScreen : UserControl, ILoginScreen
    {
        public LoginScreen()
        {
            InitializeComponent();
        }

        public void Fail(string message)
        {
            Message(message);
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

        public event Action<string> LogIn = (x) => { };
        public event Action<string> Send = (x) => { };

        private void loginClick(object sender, EventArgs e)
        {
            if (nickTextBox.Text.Length != 0)
            {
                LogIn(nickTextBox.Text);
            }
            else
            {
                Message("Ошибка ввода");
            }
        }
        private void Message(string message)
        {
            MessageBox.Show(message);
        }
       
    }
}
