using GameTemplate.Game;
using GameTemplate.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTemplate.ChessField
{
    [Serializable]
    public class ROField :IReadOnlyField
    {
        public ROField(IChessFigure[,] board)
        {
            this.board = board;
        }

        private IChessFigure[,] board;

        public IChessFigure this[Point location]
        {
            get { return board[location.X, location.Y]; }
        }
        public IChessFigure this[int x, int y]
        {
            get { return board[x, y]; }
        }
        public bool IsGameOver
        {
            get;
            private set;
        }
        public IReadOnlyList<FigureOnBoard> GetFiguresOnBoard()
        {
            List<FigureOnBoard> res = new List<FigureOnBoard>();

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int k = 0; k < board.GetLength(1); k++)
                {
                    if (board[i, k] != null)
                    {
                        res.Add(new FigureOnBoard(board[i, k], new Point(i, k)));
                    }
                }
            }

            return res.AsReadOnly();
        }    


        public IField Clone()
        {
            return new ChessField(this.GetFiguresOnBoard());
        }
    }
}
