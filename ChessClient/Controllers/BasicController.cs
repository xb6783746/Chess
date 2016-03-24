using ChessClient.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessClient.Controllers
{
    class BasicController :ISwitch
    {
        public BasicController(IMainForm mainForm, IServerFacade facade)
        {
            this.mainForm = mainForm;
            this.facade = facade;

            LoadScreen();
        }

        public virtual void Enable()
        {
            mainForm.Screen = screen;
        }

        public virtual void Disable()
        {
            mainForm.Screen = null;
        }

        protected IMainForm mainForm;
        protected IServerFacade facade;
        protected UserControl screen;

        protected virtual void LoadScreen()
        {

        }
    }
}
