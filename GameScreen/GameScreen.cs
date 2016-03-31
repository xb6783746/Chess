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
        private IReadOnlyList<FigureOnBoard> field;
        private FColor color;
        private bool YourTurn;

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
            if (YourTurn)
            {
                if (from.IsEmpty)
                {
                    from = GetGamePoint(e.Location);
                    IReadOnlyList<FigureOnBoard> q = field.Where(x => x.Location == from) as IReadOnlyList<FigureOnBoard>;

                    //render.DrawCells(field, q[0].Figure.GetCells(from, field) );
                    UpdatePic();
                    return;
                }
                if (to.IsEmpty)
                {
                    to = GetGamePoint(e.Location);
                }

                Step(new StepInfo(from, to));

                from = to = new Point();
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

        public void StartGame(IReadOnlyList<FigureOnBoard> figures, FColor color)
        {
            string cl = color.ToString();
            MessageBox.Show("Вы играете за" + cl + "цвет");
            this.color = color;

            render.UpdateField(picture, figures);
            GameBox.Image = picture;
            field = figures;
        }

        public void UpdateField(ChessState state)
        {
            render.UpdateField(picture, state.Figures);
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
