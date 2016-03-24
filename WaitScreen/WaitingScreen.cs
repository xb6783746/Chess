using ClientAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WaitScreen
{
    [Screen]
    public class WaitScreen : AbstractWaitingScreen
    {
        private Button cancelButton;

        private void InitializeComponent()
        {
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(109, 132);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(86, 31);
            this.cancelButton.TabIndex = 0;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // WaitingScreen
            // 
            this.Controls.Add(this.cancelButton);
            this.Name = "WaitingScreen";
            this.Size = new System.Drawing.Size(300, 176);
            this.ResumeLayout(false);

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        public override event Action Close;
    }
}
