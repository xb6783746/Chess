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

        private Dictionary<string, Func<IGamer>> algos;

        public AlgoRepo()
        {
            algos = new Dictionary<string, Func<IGamer>>()
            {
                {"AIRandom", () => new AIRandom()},
                {"MiniMax", () => new MiniMax(new SimpleRatingFunc())}
            };
        }

        public IGamer GetAlgo(string name)
        {
            if (algos.ContainsKey(name))
            {
                return algos[name]();
            }

            return null;
        }

        public List<string> AllNames()
        {
            return algos.Keys.ToList();
        }

    }
}
