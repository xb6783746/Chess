using GameTemplate.ChessEnums;
using GameTemplate.Game;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTemplate.Interfaces
{
    /// <summary>
    /// Интерфейс класса игрового поля
    /// </summary>
    /// <typeparam name="T">Интерфейс фигур на доске</typeparam>
    public interface IField :IReadOnlyField
    {

        /// <summary>
        /// Сделать ход из клетки с координатами from в клетку с координатами to
        /// </summary>
        /// <param name="from">Координата ходящей фигуры</param>
        /// <param name="to">Координата, куда ходит фигура с координатами from</param>
        bool MakeStep(Point from, Point to);
        /// <summary>
        /// Сделать ход согласно заданной StepInfo
        /// </summary>
        /// <param name="step">Информация о ходе</param>
        bool MakeStep(StepInfo step);
        IReadOnlyField GetReadOnlyField();

        /// <summary>
        /// Событие завершения игры
        /// </summary>
        event Action<FColor> GameOver;
    }
}
