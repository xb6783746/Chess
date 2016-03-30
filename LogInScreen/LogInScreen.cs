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
        public LogInScreen()
        {
            InitializeComponent();
        }

        private Button logInButton;
        private TextBox nickTextBox;
        private Label nickLabel;
        public void Fail(string message)
        {
            Message(message);
        }

        private void InitializeComponent()
        {
            this.logInButton = new System.Windows.Forms.Button();
            this.nickTextBox = new System.Windows.Forms.TextBox();
            this.nickLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // logInButton
            // 
            this.logInButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.logInButton.Location = new System.Drawing.Point(26, 101);
            this.logInButton.Name = "logInButton";
            this.logInButton.Size = new System.Drawing.Size(262, 81);
            this.logInButton.TabIndex = 0;
            this.logInButton.Text = "Войти";
            this.logInButton.UseVisualStyleBackColor = true;
            this.logInButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // nickTextBox
            // 
            this.nickTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nickTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nickTextBox.Location = new System.Drawing.Point(26, 54);
            this.nickTextBox.Name = "nickTextBox";
            this.nickTextBox.Size = new System.Drawing.Size(262, 38);
            this.nickTextBox.TabIndex = 2;
            // 
            // nickLabel
            // 
            this.nickLabel.AutoSize = true;
            this.nickLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nickLabel.Location = new System.Drawing.Point(19, 14);
            this.nickLabel.Name = "nickLabel";
            this.nickLabel.Size = new System.Drawing.Size(72, 37);
            this.nickLabel.TabIndex = 4;
            this.nickLabel.Text = "Ник";
            // 
            // LogInScreen
            // 
            this.Controls.Add(this.nickLabel);
            this.Controls.Add(this.nickTextBox);
            this.Controls.Add(this.logInButton);
            this.Name = "LogInScreen";
            this.Size = new System.Drawing.Size(326, 252);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void button2_Click(object sender, EventArgs e)
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

        void Message(string message)
        {
            MessageBox.Show(message);
        }

        public event Action<string> LogIn = (x) => { };
        public event Action<string> Send = (x) => { };

        public UserControl GetScreen()
        {
            return this;
        }
    }
}
