using System.Windows.Forms;
namespace LogInScreen
{
    partial class LoginScreen
    {
        /// <summary> 
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Обязательный метод для поддержки конструктора - не изменяйте 
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
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
            this.logInButton.TabIndex = 1;
            this.logInButton.Text = "Войти";
            this.logInButton.UseVisualStyleBackColor = true;
            this.logInButton.Click += new System.EventHandler(this.loginClick);
            // 
            // nickTextBox
            // 
            this.nickTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nickTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nickTextBox.Location = new System.Drawing.Point(26, 54);
            this.nickTextBox.Name = "nickTextBox";
            this.nickTextBox.Size = new System.Drawing.Size(262, 38);
            this.nickTextBox.TabIndex = 0;
            this.nickTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.nickTextBox_KeyDown);
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
            // LoginScreen
            // 
            this.Controls.Add(this.nickLabel);
            this.Controls.Add(this.nickTextBox);
            this.Controls.Add(this.logInButton);
            this.Name = "LoginScreen";
            this.Size = new System.Drawing.Size(326, 252);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button logInButton;
        private TextBox nickTextBox;
        private Label nickLabel;
    }
}
