using GameTemplate.ChessGame.ChessInterfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTemplate.Interfaces
{
    /// <summary>
    /// Интерфейс фигуры
    /// </summary>
    public interface IFigure
    {
        /// <summary>
        /// Цвет фигуры
        /// </summary>
        Color Color { get; }

        /// <summary>
        /// Может ли фигура сходить из клетки с координатами from в клетку с координатами to
        /// </summary>
        bool Step(Point from, Point to, IField<IChessFigure> field);

        /// <summary>
        /// Возвращает все клетки, доступные для хода из клетки с координатами location
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        List<Point> GetAllCells(Point location);
        List<Point> GetCells(Point location, IField<IChessFigure> field);

    }
}
