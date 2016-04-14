﻿using GameTemplate.Game;
using Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ChessClient.Interfaces
{
    public interface IServer
    {
        void LogIn(IPAddress ip, int port, string nick);
        void ChangeNick(string nick);
        void StartRandomGame();
        void StartGameWith(string gamer);
        void StartGameWithComputer(string alg);
        void WatchFor(string gamer);
        void MakeStep(StepInfo step);
        void SendMessage(ChatMessage msg);
        void Surrender();
        void GameWithAnswer(bool ans);
        void Disconnect();
        void GetOnline();
        void GetAlgos();
    }
}
