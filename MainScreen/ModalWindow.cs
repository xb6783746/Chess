using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace tmp
{
    public partial class ModalWindow : UserControl
    {
        public ModalWindow()
        {
            InitializeComponent();
        }

        public void Activate(List<string> users)
        {
            userBox.Items.Clear();

            foreach(var item in users)
            {
                userBox.Items.Add(item);      
            }

        }


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

        public event Action<string> Select;
        public event Action Cancel;
    }
}
