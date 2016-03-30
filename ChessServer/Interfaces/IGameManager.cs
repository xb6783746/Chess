using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessServer.Interfaces
{
    interface IGameManager
    {
        void RandomGame(int gamer);
        void RequestGame(int who, int gamer);
        void WatchFor(int gamerId, int gameId);

        void GameWithComputer(int gamerId, int algId);


        event Action<int> GameOver;
        event Action<int> GameStart;
    }
}
