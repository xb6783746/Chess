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
        public ROField(IChessFigure[,] board, Dictionary<IChessFigure, int> died)
        {
            this.board = board;
            //this.died = new Dictionary<IChessFigure, int>();
        }

        private IChessFigure[,] board;
        //private Dictionary<IChessFigure, int> died;

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
        //public IReadOnlyDictionary<IChessFigure, int> GetDiedFigures()
        //{
        //    throw new NotImplementedException();
        //}      


        public IField Clone()
        {
            return new ChessField(this.GetFiguresOnBoard());
        }
    }
}
