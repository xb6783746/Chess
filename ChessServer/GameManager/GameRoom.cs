using ChessServer.Interfaces;
using GameTemplate.ChessGame.ChessField;
using GameTemplate.Game;
using GameTemplate.Interfaces;
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

        public void AddWatcher(IClient watcher)
        {
            watchers.Add(watcher);
        }

        private void Update()
        {
            lock (lck)
            {
                foreach (var item in watchers)
                {
                    clientFacade.Update(
                        game.Field.GetFiguresOnBoard(), 
                        new StepInfo(new Point(), new Point()), 
                        item.Id);
                }
            }
        }
        private void GameOver(Color color)
        {
            //послать сообщение об конце игры

            RoomClosed(RoomId);
        }

        public event Action<int> RoomClosed;
    }
}
