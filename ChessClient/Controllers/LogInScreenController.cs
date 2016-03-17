using ChessClient.Interfaces;
using ChessClient.Interfaces.IControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClient.Controllers
{
    class LogInScreenController : ISwitch, ILoginScreenController
    {
        public void Enable()
        {
            throw new NotImplementedException();
        }

        public void Disable()
        {
            throw new NotImplementedException();
        }

        public void Fail(string message)
        {
            throw new NotImplementedException();
        }
    }
}
