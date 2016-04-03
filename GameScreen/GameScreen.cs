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
        private FColor color;
        private bool YourTurn;
        private Point temp;

        public void GameOver(FColor win)
        {
            string end = win == FColor.Black ? "Черные" : "Белые";
            MessageBox.Show("Победили " + end);
        }

        public void SetRender(IRender r)
        {
            render = r;
        }

        public void StartGame(IReadOnlyField figures, FColor color)
        {
            Color(color);
            YourTurn = (this.color == FColor.White);

            field = figures;
            UpdatePic();
        }

        public void UpdateField(ChessState state)
        {
            field = state.Figures;
            UpdatePic();
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


        private void Concede_Click(object sender, EventArgs e)
        {
            Concede();
        }
        private void GameBox_Click(object sender, MouseEventArgs e)
        {
            if (YourTurn)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    temp = GetGamePoint(e.Location);

                    if (field[temp] != null)
                    {
                        if (from.IsEmpty && field[temp].Color == this.color) // если пустой 1 клик
                        {
                            SelectFigure();
                            return;
                        }
                        else
                        {
                            if (field[temp].Color == this.color) // если кликнули по своей
                            {
                                SelectFigure();
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (!from.IsEmpty && !field[from].Step(from, temp, field)) // активной фигурой пытаемся пойти в пустоту
                        {
                            UpdatePic();
                        }
                    }


                    Step(new StepInfo(from, temp));
                    from = new Point();
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
            from = temp;
            render.DrawCells(picture, field, from, field[from].GetCells(from, field));
            GameBox.Image = picture;
        }
        private Point GetGamePoint(Point systemPoint)
        {
            return new Point(systemPoint.X / cellSize, systemPoint.Y / cellSize);
        }
        private void UpdatePic()
        {
            render.UpdateField(picture, field.GetFiguresOnBoard());
            GameBox.Image = picture;
        }
        private void Color(FColor color)
        {
            this.color = color;
            YourColor.Text += color.ToString();
        }


        public void Enable()
        {
           
        }
        public void Disable()
        {
            
        }


        public void GameClosed(string msg)
        {
            MessageBox.Show(msg);
        }

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
    }
}
