using GameTemplate.ChessEnums;
using GameTemplate.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessServer.Interfaces
{
    //чем больше оценка - тем лучше для белых
    interface IRatingFunc
    {
        int Rating(IReadOnlyField field, FColor color);
    }
}
