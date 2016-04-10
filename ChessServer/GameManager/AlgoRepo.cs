using ChessServer.Interfaces;
using GameTemplate.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessServer.Algorithms;

namespace ChessServer.Managers
{
    class AlgoRepo : IAlgoFactory
    {

        private Dictionary<string, Type> algos;

        public AlgoRepo()
        {
            algos = new Dictionary<string, Type>()
            {
                {"AIRandom", typeof(AIRandom)}
            };
        }

        public IGamer GetAlgo(string name)
        {
            return Activator.CreateInstance(algos[name]) as IGamer;
        }

        public List<string> AllNames()
        {
            return new List<string>() { "test" };
        }

    }
}
