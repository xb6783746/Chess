using ChessClient.Interfaces;
using ChessClient.Interfaces.IControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClient.Controllers
{
    class MainScreenController : ISwitch, IMainScreenController
    {
        public void Enable()
        {
            throw new NotImplementedException();
        }
        public void Disable()
        {
            throw new NotImplementedException();
        }

        public void Challenge(string from)
        {
            throw new NotImplementedException();
        }
        public void Message(string msg)
        {
            throw new NotImplementedException();
        }
    }
}
