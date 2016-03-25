﻿using ChessClient.Interfaces;
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
            throw new NotImplementedException();
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
            Message mesg = new Message("StartGame", null);

            SendMessage(mesg);
        }
        public void StartGameWith(string gamer)
        {
            throw new NotImplementedException();
        }
        public void MakeStep(StepInfo step)
        {
            throw new NotImplementedException();
        }
        public void SendMessage(string msg)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
        public void WatchFor(string gamer)
        {
            throw new NotImplementedException();
        }

        public void Parse(byte[] message)
        {
            lock(lck)
            {
                var stream = new MemoryStream(message);
                Message mesg = formatter.Deserialize(stream) as Message;

                clientFacade.GetType().GetMethod(mesg.Method).Invoke(clientFacade, mesg.Arguments);
            }
        }

        private void SendMessage(Message mesg)
        {
            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, mesg);

            socketListener.Send(stream.GetBuffer());
        }
    }
}