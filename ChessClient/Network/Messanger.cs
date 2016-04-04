using ChessClient.Interfaces;
using GameTemplate.Game;
using Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ChessClient.Network
{
    class Messanger : IServerFacade, IParser
    {
        public Messanger()
        {        
            formatter = new BinaryFormatter();
        }

        public void LogIn(IPAddress ip, int port, string message)
        {
            socketListener.Connect(ip, port);
            if (socketListener.IsRunning)
            {
                ChangeNick(message);
            }
        }
        public void Init(IClientFacade clientFacade, ISocketListener socketListener)
        {
            this.clientFacade = clientFacade;
            this.socketListener = socketListener;
        }

        private IClientFacade clientFacade;
        private BinaryFormatter formatter;
        private ISocketListener socketListener;
        private object lck = new object();

        public void StartRandomGame()
        {
            Message mesg = new Message("RandomGame");

            SendMessage(mesg);
        }
        public void StartGameWith(string gamer)
        {
            throw new NotImplementedException();
        }
        public void MakeStep(StepInfo step)
        {
            Message m = new Message("NewStep", step);

            SendMessage(m);
        }
        public void SendMessage(ChatMessage msg)
        {
            Message m = new Message("Message", msg);

            SendMessage(m);
        }
        public void Surrender()
        {
            throw new NotImplementedException();
        }
        public void Accept()
        {
            throw new NotImplementedException();
        }
        public void ChangeNick(string nick)
        {
            Message m = new Message("ChangeNick", nick);

            SendMessage(m);
        }
        public void WatchFor(string gamer)
        {
            throw new NotImplementedException();
        }

        public void Parse(byte[] message)
        {
            lock(lck)
            {
                Message mesg;
                try
                {
                    using (var stream = new MemoryStream(message))
                    {
                        mesg = formatter.Deserialize(stream) as Message;

                        clientFacade.GetType().GetMethod(mesg.Method).Invoke(clientFacade, mesg.Arguments);
                    }
                }
                catch(Exception e)
                {

                }
            }
        }

        private void SendMessage(Message mesg)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, mesg);

                socketListener.Send(stream.GetBuffer());
            }
        }

        public void Disconnect()
        {
            Message m = new Message("Disconnect", null);

            SendMessage(m);
        }


        public void GetOnline()
        {
            Message m = new Message("GetOnline");
            SendMessage(m);
        }
    }
}
