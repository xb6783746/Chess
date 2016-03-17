using ClientAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogInScreen
{
    [Screen]
    public class LogInScreen :UserControl, ILoginScreen
    {
        private Button button1;
        private MaskedTextBox maskedTextBox1;
        private Label label1;
        private Button logInButton;
        private MaskedTextBox ipTextBox;
        private TextBox nickTextBox;
        private Label label2;
        private Label label3;
        private TextBox portTextBox;
        private Label label4;
        private TextBox textBox1;
        public event Action<IPAddress, int, string> LogIn = (x, y, z) => { };

        public void Fail(string message)
        {
            Message(message);
        }

        private void InitializeComponent()
        {
            this.logInButton = new System.Windows.Forms.Button();
            this.ipTextBox = new System.Windows.Forms.MaskedTextBox();
            this.nickTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // logInButton
            // 
            this.logInButton.Location = new System.Drawing.Point(33, 139);
            this.logInButton.Name = "logInButton";
            this.logInButton.Size = new System.Drawing.Size(170, 59);
            this.logInButton.TabIndex = 0;
            this.logInButton.Text = "Войти";
            this.logInButton.UseVisualStyleBackColor = true;
            this.logInButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // ipTextBox
            // 
            this.ipTextBox.Location = new System.Drawing.Point(33, 42);
            this.ipTextBox.Mask = "000:000:000:000";
            this.ipTextBox.Name = "ipTextBox";
            this.ipTextBox.Size = new System.Drawing.Size(91, 20);
            this.ipTextBox.TabIndex = 1;
            // 
            // nickTextBox
            // 
            this.nickTextBox.Location = new System.Drawing.Point(33, 92);
            this.nickTextBox.Name = "nickTextBox";
            this.nickTextBox.Size = new System.Drawing.Size(170, 20);
            this.nickTextBox.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "IP-адрес";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Ник";
            // 
            // portTextBox
            // 
            this.portTextBox.Location = new System.Drawing.Point(131, 42);
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.Size = new System.Drawing.Size(72, 20);
            this.portTextBox.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(131, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Порт";
            // 
            // LogInScreen
            // 
            this.Controls.Add(this.label4);
            this.Controls.Add(this.portTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nickTextBox);
            this.Controls.Add(this.ipTextBox);
            this.Controls.Add(this.logInButton);
            this.Name = "LogInScreen";
            this.Size = new System.Drawing.Size(246, 240);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            IPAddress ip;
            int port;

            if (IPAddress.TryParse(ipTextBox.Text, out ip) && int.TryParse(portTextBox.Text, out port))
            {
                LogIn(ip, port, nickTextBox.Text);
            }
            else
            {
                Message("Ошибка ввода");
            }
        }

        void Message(string message)
        {
            MessageBox.Show(message);
        }

    }
}
