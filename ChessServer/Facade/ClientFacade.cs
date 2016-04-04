using ChessServer.Interfaces;
using GameTemplate.ChessEnums;
using GameTemplate.Game;
using GameTemplate.Interfaces;
using Network;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ChessServer.Facade
{
    [Serializable]
    class ClientFacade : IClientFacade, IParser
    {
        public ClientFacade()
        {

            formatter = new BinaryFormatter();
        }

        private IServer server;
        private IServerFacade serverFacade;

        private BinaryFormatter formatter;

        public void Init(IServer server, IServerFacade serverFacade)
        {
            this.server = server;
            this.serverFacade = serverFacade;

            server.Receive += Parse;
        }

        public void Parse(byte[] msg, int id)
        {
            try
            {
                using (var stream = new MemoryStream(msg))
                {
                    var mesg = formatter.Deserialize(stream) as Message;

                    var args = new object[mesg.Arguments.Length + 1];
                    mesg.Arguments.CopyTo(args, 0);
                    args[args.Length - 1] = id;

                    serverFacade.GetType().GetMethod(mesg.Method).Invoke(serverFacade, args);
                }
            }
            catch
            {

            }
        }

        public void Message(ChatMessage msg, int id)
        {
            var message = new Message("Message", msg);

            SendMessage(message, id);
        }
        public void LoginResult(bool result, string message, int id)
        {
            Message m = new Message("LoginResult", result, message);

            SendMessage(m, id);
        }
        public void Update(ChessState state, int id)
        {
            Message m = new Message("UpdateField", state);

            SendMessage(m, id);
        }
        public void StartGame(IReadOnlyField field, FColor color, int id)
        {
            Message m = new Message("StartGame", field, color);

            SendMessage(m, id);
        }
        public void GameOver(FColor win, int id)
        {
            var m = new Message("GameOver", win);

            SendMessage(m, id);
        }
        public void GameClosed(string message, int id)
        {
            Message m = new Message("GameClosed", message);

            SendMessage(m, id);
        }
        public void Wait(int id)
        {
            Message m = new Message("Waiting");

            SendMessage(m, id);
        }
        public void SendOnlineList(string[] online, int id)
        {
            Message m = new Message("GetListOnline", new object[]{online});

            SendMessage(m, id);
        }

        private void SendMessage(Message m, int id)
        {
            using (var stream = new MemoryStream())
            {
                try
                {   
                    formatter.Serialize(stream, m);

                    server.Send(stream.GetBuffer(), id);
                }
                catch(Exception e)
                {

                }
            }
        }

    }
}
