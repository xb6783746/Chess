using ChessClient.Interfaces;
using ChessClient.ServerImitation;
using GameTemplate.ChessGame.ChessField;
using GameTemplate.ChessGame.ChessFigures;
using GameTemplate.Game;
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

        private SimpleGame game;

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
            clientFacade.Message("В данный момент это невозможно");
        }

        public void StartRandomGame()
        {
            Task.Run(() =>
            {
                clientFacade.Waiting();

                game = new SimpleGame(autoGamer, gamer, new ChessField(new ChessFiguresPool()));

               // game.Change += () => clientFacade.UpdateField(game.Field);

                Thread.Sleep(3000);

                clientFacade.StartGame(Color.White);
            });
        }

        public void StartGameWith(string gamer)
        {
            clientFacade.Message("В данный момент это невозможно");
        }
        public void WatchFor(string gamer)
        {
            clientFacade.Message("В данный момент это невозможно");
        }
        public void MakeStep(StepInfo step)
        {
            clientFacade.Message("В данный момент это невозможно");
        }

        public void SendMessage(string msg)
        {
            clientFacade.Message(String.Format("Вы отправили \"{0}\"", msg));
        }

        public void Surrender()
        {
            clientFacade.GameOver(false);
        }

        public void Accept()
        {
            
        }

        public void Parse(byte[] message)
        {
            throw new NotImplementedException();
        }
    }
}
