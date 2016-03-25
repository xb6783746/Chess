﻿using GameTemplate.ChessGame.ChessInterfaces;
using GameTemplate.Game;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTemplate.Interfaces
{
    public interface IReadOnlyField
    {
        IChessFigure this[Point location] { get; }

        /// <summary>
        /// Получить все фигуры, находящиеся на доске
        /// </summary>
        IReadOnlyList<FigureOnBoard> GetFiguresOnBoard();

        /// <summary>
        /// Получить все вышедшие из игры фигуры
        /// </summary>
        IReadOnlyDictionary<IChessFigure, int> GetDiedFigures();

        /// <summary>
        /// Завершена ли игра
        /// </summary>
        bool IsGameOver { get; }

        /// <summary>
        /// Событие завершения игры
        /// </summary>
        event Action<Color> GameOver;
    }
}
