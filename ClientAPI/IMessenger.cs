﻿using Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientAPI
{
    public interface IMessenger
    {
        event Action<ChatMessage> Send;
        void Receive(ChatMessage message);
    }
}
