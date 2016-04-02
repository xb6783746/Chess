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
        public GameRoom(IGamer first, IGamer second, IClientFacade facade, 
            IChessFigureFactory figFactory, int roomid)
        {
            this.RoomId = roomid;
            this.clientFacade = facade;

            watchers = new List<IClient>();
            game = new SimpleGame(first, second, new ChessField(figFactory));

            game.Change += Update;
            game.GameOver += GameOver;
        }

        private IGame game;
        private List<IClient> watchers;
        private IClientFacade clientFacade;
        private object lck = new object();

        public int RoomId { get; private set; }

        public List<IClient> Watchers { get { return watchers; } }
        public void AddWatcher(IClient watcher)
        {
            watchers.Add(watcher);
        }
        public event Action<int> RoomClosed;

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
