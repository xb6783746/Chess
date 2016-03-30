using ChessServer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessServer.GameManager
{
    class GameManager :IGameManager
    {
        public void RandomGame(int gamer)
        {
            throw new NotImplementedException();
        }

        public void RequestGame(int who, int gamer)
        {
            throw new NotImplementedException();
        }

        public void WatchFor(int gamerId, int gameId)
        {
            throw new NotImplementedException();
        }

        public void GameWithComputer(int gamerId, int algId)
        {
            throw new NotImplementedException();
        }

        public event Action<int> GameOver;
        public event Action<int> GameStart;
    }
}
