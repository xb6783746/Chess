using GameTemplate.Game;
using GameTemplate.Interfaces;
using Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AITurnScreen
{
    public partial class AITurnForm : Form
    {
        public AITurnForm()
        {
            InitializeComponent();

            render = new Render();
            render.Init(pictureBox1.Width, pictureBox1.Height, GameTemplate.ChessEnums.FColor.Black);
            boxes = new PictureBox[4] { pictureBox1, pictureBox2, pictureBox3, pictureBox4 };
        }


        private Render render;
        private PictureBox[] boxes;
        public void Draw(List<StepInfo> steps, IField field)
        {
            for (int i = 0; i < steps.Count; i++)
            {
                field.MakeStep(steps[i]);

                IfInvoke( () => boxes[i].Image = render.UpdateField(field.GetFiguresOnBoard()));
            }

            Invalidate();
        }

        private void IfInvoke(Action action)
        {
            if (InvokeRequired)
            {
                Invoke(action);
            }
            else
            {
                action();
            }
        }

        private void AITurnForm_Load(object sender, EventArgs e)
        {

        }
    }
}
