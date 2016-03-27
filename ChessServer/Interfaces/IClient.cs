using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessServer.Interfaces
{
    interface IClient
    {
        public int Id { get; }
        public string Nick { get; }
    }
}
