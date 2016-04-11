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
        public GameScreen()
        {
            InitializeComponent();
            cellSize = 600 / 8;
            chatScreen1.Send += (x) => Send(x);
            from = new Point();
            picture = new Bitmap(GameBox.Height, GameBox.Width);
        }

        private IRender render;
        private Point from;
        private Bitmap picture;
        private int cellSize;
        private IReadOnlyField field;
        private StepInfo step;
        private FColor color;
        private bool yourTurn;
        private Point temp;
        private bool selected;
        private int boardSize = 7;


        public string Nick
        {
            get
            {
                return chatScreen1.Nick;
            }
            set
            {
                chatScreen1.Nick = value;
            }
        }

        public void GameOver(FColor win)
        {
            string end = win == FColor.Black ? "черные" : "белые";
            MessageBox.Show("Победили " + end);
        }
        public void SetRender(IRender r)
        {
            render = r;

        }
        public void StartGame(IReadOnlyField figures, FColor color)
        {
            render.Init(GameBox.Width, GameBox.Height, color);

            Color(color);
            yourTurn = (this.color == FColor.White);

            SetTurnLabel(FColor.White);

            field = figures;
            GameBox.Image = render.UpdateField(figures.GetFiguresOnBoard());
        }
        public void UpdateField(ChessState state)
        {
            field = state.Figures;
            step = state.LastStep;         
            yourTurn = (color == state.Turn);

            SetTurnLabel(state.Turn);

            UpdatePic(step);
        }
        public void Receive(ChatMessage message)
        {
            chatScreen1.Receive(message);
        }
        public UserControl GetScreen()
        {
            return this;
        }
        public void Enable()
        {

        }
        public void Disable()
        {
            from = new Point();
            yourTurn = false;
            selected = false;
        }
        public void GameClosed(string msg)
        {
            MessageBox.Show(msg);
        }

        public event Action<ChatMessage> Send = (x) => { };
        public event Action<StepInfo> Step = (x) => { };
        public event Action Concede = () => { };


        private void Concede_Click(object sender, EventArgs e)
        {
            Concede();
        }
        private void GameBox_Click(object sender, MouseEventArgs e)
        {
            if (yourTurn)
            {
                if (e.Button == MouseButtons.Left)
                {
                    temp = GetGamePoint(e.Location);

                    if (field[temp] != null)
                    {
                        if (!selected && field[temp].Color == this.color) // если пустой 1 клик
                        {
                            from = temp;
                            SelectFigure();
                            selected = true;
                            return;
                        }
                        else
                        {
                            if (field[temp].Color == this.color) // если кликнули по своей
                            {
                                from = temp;
                                SelectFigure();
                                selected = true;
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (selected && !field[from].Step(from, temp, field)) // активной фигурой пытаемся пойти в пустоту
                        {
                            UpdatePic();
                        }
                    }

                    if (selected)
                    {
                        Step(new StepInfo(from, temp));
                        from = new Point();
                        selected = false;
                    }
                }
                else
                {
                    from = new Point();
                    UpdatePic();
                }


            }
        }
        private void SelectFigure()
        {
            GameBox.Image = render.DrawCells(field, from, field[from].GetCells(from, field));
        }
        private Point GetGamePoint(Point systemPoint)
        {
            Point p = color == FColor.White ? 
                new Point(systemPoint.X / cellSize, systemPoint.Y / cellSize) :
                new Point(systemPoint.X / cellSize, boardSize - systemPoint.Y / cellSize);

            return p;
        }

        private void UpdatePic()
        {
           GameBox.Image = render.UpdateField(field.GetFiguresOnBoard());
        }
        private void UpdatePic(StepInfo step)
        {
            GameBox.Image = render.UpdateField(field.GetFiguresOnBoard(), step);
        }

        private void Color(FColor color)
        {
            this.color = color;
            IfInvoke(() =>
            {
                YourColor.Text ="Ваш цвет: "+ (color == FColor.White ? "Белый" : "Черный");
            });
        }
        private void SetTurnLabel(FColor color)
        {
            var txt = color == FColor.White ? "белыми" : "черными";

            IfInvoke(() =>
            {
                Turn.Text = "Ход за " + txt;
            });
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

    }
}
