using ChessServer.IdManager;
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
            idManager = new IDManager();
            this.chessPool = new ChessFiguresPool();

            this.clientFacade = clientFacade;
            this.clientManager = clientManager;

            clientManager.Disconnected += Disconnected;
            clientManager.Connected += Connected;

            waitLck = new object();
            roomLck = new object();
        }

        private IChessFigureFactory chessPool;
        private IClientFacade clientFacade;
        private IClientManager clientManager;
        private List<IClient> playerWait;
        private List<GameRoom> gameRooms;
        private IIDManager idManager;
        private IReadOnlyField startField = ChessField.Empty;

        private object waitLck;
        private object roomLck;

        public List<IClient> Watchers(int roomId)
        {
            var room = gameRooms.FirstOrDefault((x) => x.RoomId == roomId);

            return room == null ? null : room.Watchers;
        }
        public void RandomGame(IClient gamer)
        {
            clientFacade.Wait(gamer.Id);

            lock (waitLck)
            {
                playerWait.Add(gamer);
             
                while (playerWait.Count > 1)
                {
                    CreateRoom(playerWait[0], playerWait[1]);

                    playerWait.RemoveRange(0, 2);
                }
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
            //CreateRoom(gamerId, algId);
        }

        public event Action<int> GameOver = (x) => { };
        public event Action<int> GameStart = (x) => { };

        private void CreateRoom(IClient first, IClient second)
        {
            clientFacade.StartGame(startField, FColor.Black, first.Id);
            clientFacade.StartGame(startField, FColor.White, second.Id);

            var room = new GameRoom(
                first,
                second,
                clientFacade,
                chessPool,
                idManager.GetId()
                );

            room.AddWatcher(first);
            room.AddWatcher(second);

            room.RoomClosed += (x) => GameOver(x);

            lock (roomLck)
            {             
                gameRooms.Add(room);             
            }

            GameStart(room.RoomId);
        }
        private void Disconnected(IClient client)
        {
            //поиск в очереди
            lock (waitLck)
            {
                playerWait.RemoveAll((x) => x == client);
            }
            //поиск в игровых комнатах
            lock (roomLck)
            {
                var room = gameRooms.FirstOrDefault((x) => x.Gamers.Contains(client));
                if (room != null)
                {
                    room.CloseRoom(client);
                    gameRooms.Remove(room);

                    idManager.Delete(room.RoomId);
                }             
            }
            
        }
        private void Connected(IClient client)
        {

        }
       
    }
}
