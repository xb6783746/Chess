using GameTemplate.ChessGame.ChessEnums;
using GameTemplate.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTemplate.ChessGame.ChessInterfaces
{
    class IChessFigure :IFigure
    {
        public ChessFType Type { get; }
    }
}
