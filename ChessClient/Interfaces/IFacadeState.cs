using ChessClient.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClient.Interfaces
{
    interface IFacadeState :IClientFacade
    {
        event Action<ClientState> Switch;
    }
}
