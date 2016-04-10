using GameTemplate.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessServer.Interfaces
{
    interface IAlgoFactory
    {
        IGamer GetAlgo(string name);
        List<string> AllNames();
    }
}
