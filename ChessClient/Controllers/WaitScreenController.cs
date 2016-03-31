using ChessClient.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClient.Controllers
{
    class WaitScreenController : BasicController, ISwitch
    {
        public WaitScreenController(IMainForm mainForm, IServerFacade facade) :base(mainForm, facade)
        {

        }
    }
}
