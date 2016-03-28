using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network
{
    [Serializable]
    public enum ChatMessageType
    {
        Public, Private, System
    }

    [Serializable]
    public class ChatMessage
    {
        public ChatMessage(string from, string to, ChatMessageType type, string text)
        {
            this.From = from;
            this.To = to;
            this.Type = type;
            this.Text = text;
                    
        }

        public ChatMessageType Type { get; private set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Text { get; set; }

        public string Message { get { return From + ": " + Text; } }
    }
}
