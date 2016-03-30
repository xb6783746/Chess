using ChessServer.Interfaces;
using GameTemplate.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessServer.GameManager
{
    class GameManager : IGameManager
    {
        public GameManager(IChessFigureFactory chessPool, IClientFacade clientFacade)
        {
            playerWait = new List<IClient>();
            gameRooms = new List<GameRoom>();
            this.chessPool = chessPool;
            this.clientFacade = clientFacade;
        }

        private IChessFigureFactory chessPool;
        private IClientFacade clientFacade;
        private List<IClient> playerWait;
        private List<GameRoom> gameRooms;

        public void RandomGame(IClient gamer)
        {
            playerWait.Add(gamer);
            if (playerWait.Count == 2)
            {
                CreateRoom(playerWait[0], playerWait[1]);

                playerWait.Clear();
            }
        }

        public void RequestGame(IClient who, IClient gamer)
        {
            CreateRoom(who, gamer);
        }

        public void WatchFor(IClient gamerId, int gameId)
        {
            if (gameId < gameRooms.Count)
            {
                gameRooms[gameId].AddWatcher(gamerId);
            }
        }

        public void GameWithComputer(IClient gamerId, IClient algId)
        {
            CreateRoom(gamerId, algId);
        }

        public event Action<int> GameOver;
        public event Action<int> GameStart;

        private void CreateRoom(IClient first, IClient second)
        {
            clientFacade.StartGame(Color.White, first.Id);
            clientFacade.StartGame(Color.Black, second.Id);

            gameRooms.Add(new GameRoom(first.Gamer, second.Gamer, clientFacade, chessPool, gameRooms.Count + 1));

            GameStart(first.Id);
            GameStart(second.Id);
        }

    }
}
