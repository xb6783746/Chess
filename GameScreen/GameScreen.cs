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
        //private Point to;
        private Bitmap picture;
        private int cellSize = 600 / 8;
        private IReadOnlyField field;
        private FColor color;
        private bool YourTurn;
        private Point temp;

        public GameScreen()
        {
            InitializeComponent();

            chatScreen1.Send += (x) => Send(x);
            from = new Point();
            picture = new Bitmap(GameBox.Height, GameBox.Width);
        }

        private void Concede_Click(object sender, EventArgs e)
        {
            Concede();
        }

        private void GameBox_Click(object sender, MouseEventArgs e)
        {
            if (YourTurn)
            {
                temp = GetGamePoint(e.Location);
                if (from.IsEmpty && field[temp] != null)
                {
                    from = temp;

                    render.DrawCells(picture, field, field[from].GetCells(from, field));
                    UpdatePic();

                    return;
                }

                Step(new StepInfo(from, temp));
                from = new Point();
            }
        }


        public void GameOver(bool win)
        {
            string end;
            if (win)
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

        public void StartGame(IReadOnlyField figures, FColor color)
        {
            string cl = color.ToString();
            MessageBox.Show("Вы играете за" + cl + "цвет");
            this.color = color;
            YourTurn = (color == FColor.White);

            render.UpdateField(picture, figures.GetFiguresOnBoard());
            GameBox.Image = picture;
            field = figures;
        }

        public void UpdateField(ChessState state)
        {
            render.UpdateField(picture, state.Figures.GetFiguresOnBoard());
            UpdatePic();

            field = state.Figures;
            YourTurn = (color == state.Turn);
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

        private Point GetGamePoint(Point systemPoint)
        {
            return new Point(systemPoint.X / cellSize, systemPoint.Y / cellSize);
        }
        private void UpdatePic()
        {
            GameBox.Image = picture;
        }
    }
}
