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
using GameTemplate.ChessGame.ChessField;
using GameTemplate.ChessEnums;

namespace GameScreen
{
    public partial class GameScreen : UserControl, IGameScreen
    {

        private IRender render;
        private Point from;
        private Point to;
        private Bitmap picture;
        private int cellSize = 600 / 8;

        public GameScreen()
        {
            InitializeComponent();

            chatScreen1.Send += Send;
            from = to = new Point();
            picture = new Bitmap(GameBox.Height, GameBox.Width);
        }

        private void Concede_Click(object sender, EventArgs e)
        {
            Concede();
        }

        private void GameBox_Click(object sender, MouseEventArgs e)
        {
            if (from.IsEmpty)
            {
                from = new Point(e.X / cellSize, e.Y / cellSize);
                return;
            }
            if (to.IsEmpty)
            {
                to = new Point(e.X / cellSize, e.Y / cellSize);
            }

            Step(new StepInfo(from, to));

            from = to = new Point();
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

        public void StartGame(IReadOnlyList<FigureOnBoard> figures, FColor color)
        {
            string cl = color.ToString();
            MessageBox.Show("Вы играете за" + cl + "цвет");

            render.UpdateField(picture, figures);
            GameBox.Image = picture;
        }

        public void UpdateField(ChessState state)
        {
            render.UpdateField(picture, state.Figures);
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
