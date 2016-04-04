using ChessClient.Imitation;
using ChessClient.Interfaces;
using GameTemplate.ChessGame.ChessField;
using GameTemplate.ChessGame.ChessFigures;
using GameTemplate.Game;
using GameTemplate.Interfaces;
using Network;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChessClient.Network
{
    class ServerImitation :IServerFacade, IParser
    {
        public ServerImitation()
        {
            autoGamer = new SimpleGamer();
            gamer = new GamerImitation();
            
        }

        private IClientFacade clientFacade;

        private IGame game;

        private SimpleGamer autoGamer;
        private GamerImitation gamer;

        public void Init(IClientFacade clientFacade)
        {
            this.clientFacade = clientFacade;
        }

        public void LogIn(IPAddress ip, int port, string nick)
        {
            Task.Run(() =>
                {
                    Thread.Sleep(1000);
                    clientFacade.LoginResult(true, "");
                }
            );
        }

        public void ChangeNick(string nick)
        {
            //clientFacade.Message("В данный момент это невозможно");
        }

        public void StartRandomGame()
        {
            Task.Run(() =>
            {
                //clientFacade.Waiting();

                //game = InitGame(gamer, autoGamer);

                //game.Change += () => clientFacade.UpdateField(game.Field, new StepInfo(new Point(), new Point()));
                //game.GameOver += (x) => clientFacade.GameOver(x == gamer.Color);

                //Thread.Sleep(3000);

                //clientFacade.StartGame(Color.White);
            });
        }

        public void StartGameWith(string gamer)
        {
           // clientFacade.Message("В данный момент это невозможно");
        }
        public void WatchFor(string gamer)
        {
            //clientFacade.Message("В данный момент это невозможно");
        }
        public void MakeStep(StepInfo step)
        {
            gamer.NewStep(step);
        }
        public void SendMessage(ChatMessage msg)
        {
            clientFacade.Message(msg);
        }
        public void Surrender()
        {
            //clientFacade.GameOver(false);
        }
        public void Accept()
        {
            //clientFacade.Message("В данный момент это невозможно");
        }
        public void Parse(byte[] message)
        {
            throw new NotImplementedException();
        }

        private IGame InitGame(IGamer first, IGamer second)
        {
            return new SimpleGame(first, second, new ChessField(new ChessFiguresPool()));
        }


        public void Disconnect()
        {
            throw new NotImplementedException();
        }


        public void GetOnline()
        {
            
        }
    }
}
