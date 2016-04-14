using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessServer.Interfaces;
using ChessServer.IdManager;
using ChessServer.Clients;
using Log;

namespace ChessServer.Managers
{
    class ClientManager : IClientManager
    {

        public ClientManager(IClientFacade clientFacade, IServer server)
        {
            clients = new Dictionary<int, IClient>();
            withoutNick = new Dictionary<int, IClient>();

            this.clientFacade = clientFacade;
            this.server = server;

            Connected += (x) => ConnectNotify();
            Connected += (x) => Logger.Instance.Log(LogLevel.Info, x.Nick + " подключился");

            server.Connected += NewSocketConnect;
            server.Disconnected += Disconnect;

            forbidden = new string[] 
            { 
                "System"
            };
        }

        private Dictionary<int, IClient> withoutNick;
        private Dictionary<int, IClient> clients;
        private IClientFacade clientFacade;
        private IServer server;
        private string[] forbidden;
        private object lck = new object();

        public void ChangeNick(string nick, int id)
        {
            lock (lck)
            {
                if (withoutNick.ContainsKey(id))
                {
                    var tmp = clients.FirstOrDefault((x) => x.Value.Nick == nick).Value;
                    if (tmp != null || forbidden.Contains(nick))
                    {
                        withoutNick.Remove(id);
                        clientFacade.LoginResult(false, "Игрок с таким ником уже существует", id);
                        server.Disconnect(id);

                        return;
                    }

                    clients.Add(id, withoutNick[id]);
                    withoutNick.Remove(id);

                    clients[id].Nick = nick;

                    clientFacade.LoginResult(true, nick, id);
                    Connected(clients[id]);
                }

                clients[id].Nick = nick;
            }
        }
        public IClient GetClient(int id)
        {
            if (clients.ContainsKey(id))
            {
                return clients[id];
            }

            return null;
        }
        public IClient GetClient(string nick)
        {
            return clients.FirstOrDefault((x) => x.Value.Nick == nick).Value;
        }
        public string[] GetOnlineClient(int id)
        {
            string[] online = new string[clients.Count];
            List<IClient> temp = clients.Values.ToList();
            for (int i = 0; i < online.Length; i++)
            {
                online[i] = temp[i].Nick;
            }

            return online;
        }

        public event Action<IClient> Connected = (x) => { };
        public event Action<IClient> Disconnected = (x) => { };

        private void NewSocketConnect(int id)
        {
            IClient client = new Client(id, clientFacade);
            lock (lck)
            {
                withoutNick.Add(id, client);
            }
        }

        private void Disconnect(int id)
        {
            if (clients.ContainsKey(id))
            {
                Logger.Instance.Log(LogLevel.Info, clients[id].Nick + " отключился");

                Disconnected(clients[id]);

                lock (lck)
                {
                    clients.Remove(id);
                }
            }
        }

        private void ConnectNotify()
        {
            foreach (var client in clients.Values)
            {
                clientFacade.SendOnlineList(GetOnlineClient(client.Id), client.Id);
            }
        }
    }
}
