using ChessServer.Interfaces;
using GameTemplate.ChessEnums;
using GameTemplate.ChessGame.ChessField;
using GameTemplate.Game;
using GameTemplate.Interfaces;
using Network;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessServer.Managers
{
    [Serializable]
    class GameRoom
    {
        private GameRoom(IClientFacade facade, int roomid)
        {
            this.RoomId = roomid;
            this.clientFacade = facade;

            Gamers = new List<IClient>();
            watchers = new List<IClient>();
        }

        public GameRoom(IClient first, IGamer second, IClientFacade facade, 
            IChessFigureFactory figFactory, int roomid) :this(facade, roomid)
        {
           
            game = new SimpleGame(first.Gamer, second, new ChessField(figFactory));

            game.Change += Update;
            game.GameOver += GameOver;

            Gamers.Add(first);

            watchers = new List<IClient>(Gamers);
        }
        public GameRoom(IClient first, IClient second, IClientFacade facade,
            IChessFigureFactory figFactory, int roomid) : this(facade, roomid)
        {
            game = new SimpleGame(first.Gamer, second.Gamer, new ChessField(figFactory));

            game.Change += Update;
            game.GameOver += GameOver;

            Gamers.Add(first);
            Gamers.Add(second);

            watchers = new List<IClient>(Gamers);
        }

        private IGame game;
        private List<IClient> watchers;
        private IClientFacade clientFacade;
        private object lck = new object();

        public List<IClient> Gamers { get; private set; }
        public int RoomId { get; private set; }

        public List<IClient> Watchers { get { return watchers; } }
        public void AddWatcher(IClient watcher)
        {
            watchers.Add(watcher);
        }
        public void CloseRoom(IClient disconnected)
        {
            game.StopGame();

            var clients = watchers.Where((x) => x != disconnected);
            foreach (var client in clients)
            {
                clientFacade.GameClosed(disconnected.Nick + " вышел из игры", client.Id);
            }

            RoomClosed(this.RoomId);
        }
        public event Action<int> RoomClosed = (x) => { };

        private void Update()
        {
            lock (lck)
            {
                foreach (var item in watchers)
                {
                    clientFacade.Update(
                        GetState(),
                        item.Id);
                }
            }
        }
        private void GameOver(FColor color)
        {
            foreach (var item in watchers)
            {
                clientFacade.GameOver(color, item.Id);
            }

            RoomClosed(RoomId);
        }
        private ChessState GetState()
        {
            return new ChessState(
                game.Field, 
                game.LastStep, 
                game.Turn);
        }       
    }
}
