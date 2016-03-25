using GameTemplate.ChessGame.ChessEnums;
using GameTemplate.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTemplate.ChessGame.ChessInterfaces
{
    public interface IChessFigure :IFigure
    {
        ChessFType Type { get; }
    }
}
