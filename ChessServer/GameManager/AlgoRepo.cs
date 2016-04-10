using ChessServer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessServer.Managers
{
    class AlgoRepo : IAlgoFactory
    {
        public GameTemplate.Interfaces.IGamer GetAlgo(string name)
        {
            throw new NotImplementedException();
        }

        public List<string> AllNames()
        {
            return new List<string>() { "test" };
        }
    }
}
