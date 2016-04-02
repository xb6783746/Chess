using ChessServer.Interfaces;
using GameTemplate.ChessGame.ChessField;
using GameTemplate.ChessGame.ChessFigures;
using GameTemplate.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessServer.Managers
{
    class GameManager : IGameManager
    {
        public GameManager(IClientFacade clientFacade)
        {
            playerWait = new List<IClient>();
            gameRooms = new List<GameRoom>();
            this.chessPool = new ChessFiguresPool();
            this.clientFacade = clientFacade;
            key = new object();
        }

        private IChessFigureFactory chessPool;
        private IClientFacade clientFacade;
        private List<IClient> playerWait;
        private List<GameRoom> gameRooms;
        private IReadOnlyField startField = ChessField.Empty;
        private object key;

        public void RandomGame(IClient gamer)
        {
            playerWait.Add(gamer);

            clientFacade.Wait(gamer.Id);
            if (playerWait.Count == 2)
            {
                CreateRoom(playerWait[0], playerWait[1]);

                playerWait.Clear();
            }
        }

        public void RequestGame(IClient who, IClient gamer)
        {
           // CreateRoom(who, gamer);
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

        public event Action<int> GameOver = (x) => { };
        public event Action<int> GameStart = (x) => { };

        private void CreateRoom(IClient first, IClient second)
        {
            lock (key)
            {
                clientFacade.StartGame(startField, Color.White, first.Id);
                clientFacade.StartGame(startField, Color.Black, second.Id);

                var room = new GameRoom(first.Gamer, second.Gamer, clientFacade, chessPool, gameRooms.Count + 1);
                room.AddWatcher(first);
                room.AddWatcher(second);

                gameRooms.Add(room);

                GameStart(first.Id);
                GameStart(second.Id);
            }
        }

    }
}
