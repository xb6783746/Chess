using System.Drawing;
using System.Windows.Forms;
namespace WaitScreen
{
    partial class WaitingScreen
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
        private Label waitLabel;
        private PictureBox animateBox;
        private Button cancelButton;
        private void InitializeComponent()
        {
            this.cancelButton = new System.Windows.Forms.Button();
            this.waitLabel = new System.Windows.Forms.Label();
            this.animateBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.animateBox)).BeginInit();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Enabled = false;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Location = new System.Drawing.Point(128, 197);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(157, 68);
            this.cancelButton.TabIndex = 0;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelClick);
            // 
            // waitLabel
            // 
            this.waitLabel.AutoSize = true;
            this.waitLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.waitLabel.Location = new System.Drawing.Point(128, 25);
            this.waitLabel.Name = "waitLabel";
            this.waitLabel.Size = new System.Drawing.Size(157, 31);
            this.waitLabel.TabIndex = 1;
            this.waitLabel.Text = "Подождите";
            // 
            // animateBox
            // 
            this.animateBox.Location = new System.Drawing.Point(144, 71);
            this.animateBox.Name = "animateBox";
            this.animateBox.Size = new System.Drawing.Size(120, 120);
            this.animateBox.TabIndex = 2;
            this.animateBox.TabStop = false;
            // 
            // WaitingScreen
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.animateBox);
            this.Controls.Add(this.waitLabel);
            this.Controls.Add(this.cancelButton);
            this.Name = "WaitingScreen";
            this.Size = new System.Drawing.Size(415, 295);
            ((System.ComponentModel.ISupportInitialize)(this.animateBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
