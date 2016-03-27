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
    class ClientFacade :IClientFacade, IParser
    {
        public ClientFacade(IServer server, IServerFacade serverFacade)
        {
            this.server = server;
            this.serverFacade = serverFacade;

            formatter = new BinaryFormatter();
        }

        private IServer server;
        private IServerFacade serverFacade;

        private BinaryFormatter formatter;

        public void SendMessage(string msg, int id)
        {
            using (var stream = new MemoryStream())
            {
                var message = new Message("Message", msg);
                formatter.Serialize(stream, message);

                server.Send(stream.GetBuffer(), id);
            }
        }

        public void Parse(byte[] msg, int id)
        {
            using (var stream = new MemoryStream(msg))
            {
                var mesg = formatter.Deserialize(stream) as Message;

                var args = new object[mesg.Arguments.Length + 1];
                mesg.Arguments.CopyTo(args, 1);
                args[0] = id;

                serverFacade.GetType().GetMethod(mesg.Method).Invoke(serverFacade, args);
            }
        }
    }
}
