using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientAPI;
using GameTemplate;
using GameTemplate.Game;
using GameTemplate.Interfaces;
using Network;

namespace GameScreen
{
    public partial class GameScreen : UserControl, IGameScreen
    {

        private IRender render;
        private Point? from;
        private Point? to;
        private Bitmap picture;

        public GameScreen()
        {
            InitializeComponent();

            chatScreen1.Send += Send;
            from = to = null;
            picture = new Bitmap(GameBox.Height, GameBox.Width);
        }

        private void Concede_Click(object sender, EventArgs e)
        {
            Concede();
        }

        private void GameBox_Click(object sender, MouseEventArgs e)
        {
            if (from == null)
            {
                from = e.Location;
                return;
            }
            if (to == null)
            {
                to = e.Location;
            }

            Step(new StepInfo(from.Value, to.Value));

            from = to = null;
        }


        public void GameOver(bool win)
        {
            string end;
            if(win)
            {
                end = "Выиграли";
            }
            else
            {
                end = "Проиграли";
            }
            MessageBox.Show("Вы " + end);
        }

        public void SetRender(IRender r)
        {
            render = r;
        }

        public void StartGame(Color color)
        {
            MessageBox.Show("Вы играете за {0} цвет", color.ToString());
        }

        public void UpdateField(IReadOnlyField f)
        {
            render.UpdateField(picture, f);
            GameBox.Image = picture;
        }

        public void Receive(ChatMessage message)
        {
            chatScreen1.Receive(message);
        }

        public UserControl GetScreen()
        {
            return this;
        }


        public event Action<ChatMessage> Send;
        public event Action<StepInfo> Step;
        public event Action Concede;
    }
}
