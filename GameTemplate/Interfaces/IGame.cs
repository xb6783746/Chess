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
    /// Интерфейс класса игры
    /// </summary>
    /// <typeparam name="T">Интерфейс фигур на доске</typeparam>
    public interface IGame
    {
        /// <summary>
        /// Цвет игрока, который ходил в данный момент
        /// </summary>
        FColor CurrentColor { get; }
        /// <summary>
        /// Игровое поле
        /// </summary>
        IReadOnlyField Field { get; }
        FColor Turn { get; }
        StepInfo LastStep { get; }

        /// <summary>
        /// Событие изменения состояния игры
        /// </summary>
        event Action Change;
        /// <summary>
        /// Событие завершения игры
        /// </summary>
        event Action<FColor> GameOver;
    }
}
