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
        private class Pair
        {
            public Pair(IClient who, IClient gamer)
            {
                this.who = who;
                this.gamer = gamer;
            }

            private IClient who;
            private IClient gamer;

            public IClient Who
            {
                get { return who; }
            }
            public IClient Gamer
            {
                get { return gamer; }
            }

        }
        public GameManager(IClientFacade clientFacade, IClientManager clientManager, IAlgoFactory algos)
        {
            playerWait = new List<IClient>();
            gameRooms = new List<GameRoom>();
            idManager = new IDManager();
            friends = new List<Pair>();

            this.chessPool = new ChessFiguresPool();

            this.clientFacade = clientFacade;
            this.clientManager = clientManager;
            this.algos = algos;

            clientManager.Disconnected += Disconnected;
            clientManager.Connected += Connected;

            waitLck = new object();
            roomLck = new object();
            friendsLock = new object();
        }

        private IAlgoFactory algos;
        private IChessFigureFactory chessPool;
        private IClientFacade clientFacade;
        private IClientManager clientManager;
        private List<IClient> playerWait;
        private List<GameRoom> gameRooms;
        private List<Pair> friends;
        private IIDManager idManager;
        //private IReadOnlyField startField = ChessField.Empty;

        private object waitLck;
        private object roomLck;
        private object friendsLock;

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
            lock (friendsLock)
            {
                friends.Add(new Pair(who, gamer));
            }

            clientFacade.Wait(who.Id);
            clientFacade.Challenge(who.Nick, gamer.Id);
        }
        public void GameWithAnswer(bool ans, IClient gamer)
        {
            lock (friendsLock)
            {
                var pair = friends.FirstOrDefault((x) => x.Gamer == gamer);

                if (pair != null)
                {
                    if (ans)
                    {
                        clientFacade.Wait(gamer.Id);

                        CreateRoom(pair.Who, pair.Gamer);
                    }
                    else
                    {
                        clientFacade.StopWait(pair.Who.Id);
                    }
                }
            }
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
           // clientFacade.StartGame(startField, FColor.Black, first.Id);
           // clientFacade.StartGame(startField, FColor.White, second.Id);

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
            //поиск в списке приглашенных
            lock (friendsLock)
            {
                var pair = friends.FirstOrDefault((x) => x.Who == client || x.Gamer == client);

                if (pair != null)
                {
                    friends.Remove(pair);

                    if (pair.Who != client)
                    {
                        clientFacade.StopWait(pair.Who.Id);
                    }
                }
            }
            
        }
        private void Connected(IClient client)
        {

        }


        public string[] GetAlgoList()
        {
            return algos.AllNames().ToArray();
        }
    }
}
