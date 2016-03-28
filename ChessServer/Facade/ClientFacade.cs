using ChessServer.Interfaces;
using Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ChessServer.Facade
{
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
        public void Message(ChatMessage msg, int id)
        {
            var message = new Message("Message", msg);

            SendMessage(message, id);
        }

        public void Parse(byte[] msg, int id)
        {
            using (var stream = new MemoryStream(msg))
            {
                var mesg = formatter.Deserialize(stream) as Message;

                var args = new object[mesg.Arguments.Length + 1];
                mesg.Arguments.CopyTo(args, 0);
                args[ args.Length - 1] = id;

                serverFacade.GetType().GetMethod(mesg.Method).Invoke(serverFacade, args);
            }
        }


        public void LoginResult(bool result, string message, int id)
        {
            Message m = new Message("LoginResult", result, message);

            SendMessage(m, id);
        }

        private void SendMessage(Message m, int id)
        {
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, m);

                server.Send(stream.GetBuffer(), id);
            }
        }
    }
}
