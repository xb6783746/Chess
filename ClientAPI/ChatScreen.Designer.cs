namespace ClientAPI
{
    partial class ChatScreen
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
            this.sendButton = new System.Windows.Forms.Button();
            this.messageBox = new System.Windows.Forms.TextBox();
            this.chatWindow = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // sendButton
            // 
            this.sendButton.Location = new System.Drawing.Point(223, 179);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(73, 21);
            this.sendButton.TabIndex = 8;
            this.sendButton.Text = "Отправить";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // messageBox
            // 
            this.messageBox.Location = new System.Drawing.Point(0, 179);
            this.messageBox.Name = "messageBox";
            this.messageBox.Size = new System.Drawing.Size(217, 20);
            this.messageBox.TabIndex = 7;
            this.messageBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.messageBox_KeyDown);
            // 
            // chatWindow
            // 
            this.chatWindow.FormattingEnabled = true;
            this.chatWindow.Location = new System.Drawing.Point(0, 0);
            this.chatWindow.Name = "chatWindow";
            this.chatWindow.Size = new System.Drawing.Size(301, 173);
            this.chatWindow.TabIndex = 9;
            this.chatWindow.SelectedIndexChanged += new System.EventHandler(this.chatWindow_SelectedIndexChanged);
            // 
            // ChatScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chatWindow);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.messageBox);
            this.Name = "ChatScreen";
            this.Size = new System.Drawing.Size(301, 209);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.TextBox messageBox;
        private System.Windows.Forms.ListBox chatWindow;
    }
}
