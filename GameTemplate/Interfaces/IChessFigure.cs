using GameTemplate.ChessEnums;
using GameTemplate.ChessGame.ChessEnums;
using GameTemplate.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTemplate.Interfaces
{
    public interface IChessFigure
    {
        ChessFType Type { get; }
        /// <summary>
        /// Цвет фигуры
        /// </summary>
        FColor Color { get; }

        /// <summary>
        /// Может ли фигура сходить из клетки с координатами from в клетку с координатами to
        /// </summary>
        bool Step(Point from, Point to, IReadOnlyField field);
        /// <summary>
        /// Возвращает все клетки, доступные для хода из клетки с координатами location
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        List<Point> GetCells(Point location, IReadOnlyField field);
    }
}
