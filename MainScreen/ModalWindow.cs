using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainScreen
{
    public partial class ModalWindow : UserControl
    {
        public ModalWindow()
        {
            InitializeComponent();
        }

        public void Activate(string[] users)
        {
            this.users = users;

            UpdateListBox(users);

        }

        private string[] users;

        public event Action<string> Select = (x) => { };
        public event Action Cancel = () => { };

        private void acceptButton_Click(object sender, EventArgs e)
        {
            Select(inputBox.Text);
        }
        private void cancelButton_Click(object sender, EventArgs e)
        {
            Cancel();
        }
        private void userBox_SelectedValueChanged(object sender, EventArgs e)
        {
            inputBox.Text = userBox.SelectedItem as string;
        }
        private void inputBox_TextChanged(object sender, EventArgs e)
        {
            //string[] match = users.Where((x) => x.Contains(inputBox.Text)).ToArray();

            //if (match.Length == 0)
            //{
            //    match = users;
            //}

            //UpdateListBox(match);
        }
        private void UpdateListBox(string[] users)
        {
            userBox.Items.Clear();

            foreach (var item in users)
            {
                userBox.Items.Add(item);
            }
        }
    }
}
