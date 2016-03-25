using ChessClient.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

        protected Type GetScreenType(string dir, Type interfaceType)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + dir;
            string[] files = Directory.GetFiles(path, "*.dll");
            Type screen = null;
            bool ok = false;

            for (int i = 0; i < files.Length && !ok; i++)
            {
                var assembly = Assembly.LoadFile(files[i]);

                screen = assembly
                    .GetTypes()
                    .FirstOrDefault((x) => 
                        x.GetInterfaces().Contains(interfaceType)
                        );

                ok = screen != null;
             
            }

            return screen;
        }
    }
}
