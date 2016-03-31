using GameTemplate.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTemplate.Game
{
    /// <summary>
    /// Представление фигуры в контексте игрового поля
    /// </summary>
    /// <typeparam name="T">Интерфейс фигур на доске</typeparam>
    [Serializable]
    public struct FigureOnBoard
    {
        public FigureOnBoard(IChessFigure figure, Point location)
        {
            this._figure = figure;
            this._location = location;
        }

        private IChessFigure _figure;      
        private Point _location;

        /// <summary>
        /// Экземпляр фигуры
        /// </summary>
        public IChessFigure Figure { get { return _figure; } }

        /// <summary>
        /// Координата фигуры
        /// </summary>
        public Point Location { get { return _location; } }
    }
}
