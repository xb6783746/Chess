using GameTemplate.Interfaces;
using GameTemplate.Game;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameTemplate.ChessEnums;

namespace GameTemplate.Interfaces
{
    public interface IReadOnlyField
    {
        IChessFigure this[Point location] { get; }
        IChessFigure this[int x, int y] { get; }
        /// <summary>
        /// Завершена ли игра
        /// </summary>
        bool IsGameOver { get; }

        /// <summary>
        /// Получить все фигуры, находящиеся на доске
        /// </summary>
        IReadOnlyList<FigureOnBoard> GetFiguresOnBoard();

        IField Clone();
    }
}
