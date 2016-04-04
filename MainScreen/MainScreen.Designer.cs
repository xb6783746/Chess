﻿using ClientAPI;
using System.Windows.Forms;
namespace MainScreen.NewFolder1
{
    partial class MainScreen
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
            this.startRandomGameButton = new System.Windows.Forms.Button();
            this.gameWithButton = new System.Windows.Forms.Button();
            this.watchForButton = new System.Windows.Forms.Button();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.nickLabel = new System.Windows.Forms.Label();
            this.chatScreen = new ClientAPI.ChatScreen();
            this.usersListBox = new System.Windows.Forms.ListBox();
            this.window = new ModalWindow();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // startRandomGameButton
            // 
            this.startRandomGameButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startRandomGameButton.Location = new System.Drawing.Point(322, 26);
            this.startRandomGameButton.Name = "startRandomGameButton";
            this.startRandomGameButton.Size = new System.Drawing.Size(127, 44);
            this.startRandomGameButton.TabIndex = 3;
            this.startRandomGameButton.Text = "Случайная игра";
            this.startRandomGameButton.UseVisualStyleBackColor = true;
            this.startRandomGameButton.Click += new System.EventHandler(this.startRandomGameButton_Click);
            // 
            // gameWithButton
            // 
            this.gameWithButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gameWithButton.Location = new System.Drawing.Point(322, 76);
            this.gameWithButton.Name = "gameWithButton";
            this.gameWithButton.Size = new System.Drawing.Size(127, 44);
            this.gameWithButton.TabIndex = 5;
            this.gameWithButton.Text = "Играть с другом";
            this.gameWithButton.UseVisualStyleBackColor = true;
            this.gameWithButton.Click += new System.EventHandler(this.gameWithButton_Click);
            // 
            // watchForButton
            // 
            this.watchForButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.watchForButton.Location = new System.Drawing.Point(322, 126);
            this.watchForButton.Name = "watchForButton";
            this.watchForButton.Size = new System.Drawing.Size(127, 44);
            this.watchForButton.TabIndex = 6;
            this.watchForButton.Text = "Наблюдать за чужой игрой";
            this.watchForButton.UseVisualStyleBackColor = true;
            this.watchForButton.Click += new System.EventHandler(this.watchForButton_Click);
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.nickLabel);
            this.mainPanel.Controls.Add(this.chatScreen);
            this.mainPanel.Controls.Add(this.usersListBox);
            this.mainPanel.Controls.Add(this.watchForButton);
            this.mainPanel.Controls.Add(this.gameWithButton);
            this.mainPanel.Controls.Add(this.startRandomGameButton);
            this.mainPanel.Location = new System.Drawing.Point(14, 12);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(529, 486);
            this.mainPanel.TabIndex = 7;
            // 
            // nickLabel
            // 
            this.nickLabel.AutoSize = true;
            this.nickLabel.Location = new System.Drawing.Point(3, 10);
            this.nickLabel.Name = "nickLabel";
            this.nickLabel.Size = new System.Drawing.Size(35, 13);
            this.nickLabel.TabIndex = 9;
            this.nickLabel.Text = "label1";
            // 
            // chatScreen
            // 
            this.chatScreen.Location = new System.Drawing.Point(6, 257);
            this.chatScreen.Name = "chatScreen";
            this.chatScreen.Nick = null;
            this.chatScreen.Size = new System.Drawing.Size(301, 206);
            this.chatScreen.TabIndex = 8;
            // 
            // usersListBox
            // 
            this.usersListBox.FormattingEnabled = true;
            this.usersListBox.Location = new System.Drawing.Point(6, 26);
            this.usersListBox.Name = "usersListBox";
            this.usersListBox.Size = new System.Drawing.Size(299, 225);
            this.usersListBox.TabIndex = 7;
            // 
            // window
            // 
            this.window.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.window.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.window.Location = new System.Drawing.Point(50, 49);
            this.window.Name = "window";
            this.window.Size = new System.Drawing.Size(437, 377);
            this.window.TabIndex = 8;
            this.window.Visible = false;
            // 
            // MainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.window);
            this.Controls.Add(this.mainPanel);
            this.Name = "MainScreen";
            this.Size = new System.Drawing.Size(548, 501);
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Button startRandomGameButton;
        private Button gameWithButton;
        private Button watchForButton;
        private Panel mainPanel;
        private ListBox usersListBox;
        private ChatScreen chatScreen;
        private Label nickLabel;
        private ModalWindow window;
    }
}
