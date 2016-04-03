using ChessServer.Interfaces;
using GameTemplate.ChessEnums;
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
        public GameManager(IClientFacade clientFacade, IClientManager clientManager)
        {
            playerWait = new List<IClient>();
            gameRooms = new List<GameRoom>();
            this.chessPool = new ChessFiguresPool();

            this.clientFacade = clientFacade;
            this.clientManager = clientManager;

            clientManager.Disconnected += Disconnected;
            clientManager.Connected += Connected;

            key = new object();
        }

        private IChessFigureFactory chessPool;
        private IClientFacade clientFacade;
        private IClientManager clientManager;
        private List<IClient> playerWait;
        private List<GameRoom> gameRooms;
        private IReadOnlyField startField = ChessField.Empty;
        private object key;

        public List<IClient> Watchers(int roomId)
        {
            var room = gameRooms.FirstOrDefault((x) => x.RoomId == roomId);

            return room == null ? null : room.Watchers;
        }
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
                clientFacade.StartGame(startField, FColor.Black, first.Id);
                clientFacade.StartGame(startField, FColor.White, second.Id);

                var room = new GameRoom(
                    first, 
                    second, 
                    clientFacade, 
                    chessPool, 
                    gameRooms.Count + 1
                    );

                room.AddWatcher(first);
                room.AddWatcher(second);

                gameRooms.Add(room);

                GameStart(room.RoomId);
            }
        }
        private void Disconnected(IClient client)
        {
            //поиск в очереди


            //поиск в игровых комнатах

            var room = gameRooms.FirstOrDefault((x) => x.Gamers.Contains(client));
            if (room != null)
            {
                room.CloseRoom(client);
            }
        }
        private void Connected(IClient client)
        {

        }
       
    }
}
