using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessServer.Interfaces
{
    interface IGameManager
    {
        List<IClient> Watchers(int roomId);

        void RandomGame(IClient gamer);
        void RequestGame(IClient who, IClient gamer);
        void WatchFor(IClient gamer, int game);

        void GameWithComputer(IClient gamer, IClient alg);


        event Action<int> GameOver;
        event Action<int> GameStart;
    }
}
