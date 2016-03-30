using ChessServer.Interfaces;
using GameTemplate.ChessGame.ChessField;
using GameTemplate.ChessGame.ChessInterfaces;
using GameTemplate.Game;
using GameTemplate.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessServer.GameManager
{
    class GameRoom
    {
        public GameRoom(IGamer first, IGamer second, IClientFacade facade, IChessFigureFactory figFactory, int roomid)
        {
            this.RoomId = roomid;

            watchers = new List<int>();
            game = new SimpleGame(first, second, new ChessField(figFactory));

            game.Change += Update;
            game.GameOver += GameOver;
        }

        private IGame game;
        private List<int> watchers;
        private IClientFacade clientFacade;
        private object lck = new object();

        public int RoomId { get; private set; }

        private void Update()
        {
            lock (lck)
            {
                foreach (var item in watchers)
                {
                    clientFacade.Update(
                        game.Field, 
                        new StepInfo(new Point(), new Point()), 
                        item);
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
