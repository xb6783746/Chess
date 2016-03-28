using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessServer.Interfaces;
using ChessServer.IdManager;
using ChessServer.Clients;

namespace ChessServer.Managers
{
    class ClientManager : IClientManager
    {
        //private IDManager idManager;
        private Dictionary<int, IClient> clients;
        private IClientFacade clientFacade;

        public ClientManager(IClientFacade clientFacade)
        {
            //this.idManager = idManager;
            clients = new Dictionary<int, IClient>();
            this.clientFacade = clientFacade;
        }

        public int Registration(int id)
        {
            //int id = idManager.GetId();
            IClient client = new Client(id, clientFacade);
            clients.Add(id, client);

            client.LoginResult(true, "");
            Connected(client);

            return id;
        }

        public void Disconnect(int id)
        {
            Disconnected(clients[id]);
        }

        public event Action<IClient> Connected = (x) => { };

        public event Action<IClient> Disconnected = (x) => { };


        public void ChangeNick(string nick, int id)
        {
            clients[id].Nick = nick;
        }
    }
}
