using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network
{
    [Serializable]
    public class Message
    {
        public Message(string method, params object[] args)
        {
            this.Arguments = args;
            this.Method = method;
        }

        public object[] Arguments { get; private set; }
        public string Method { get; private set; }
    }
}
