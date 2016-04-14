using ChessClient.Controllers;
using ChessClient.Interfaces;
using ChessClient.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessClient
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var mainForm = new MainForm();
            var serverFacade = new ServerProxy();               
            var screenManager = new ScreenManager(mainForm,  serverFacade);
            var clientFacade = new ClientFacade(screenManager);
           
            var socketListener = new SocketListener(clientFacade);

            serverFacade.Init(clientFacade, socketListener);
            socketListener.SetParser(serverFacade);
                     
            Application.Run(mainForm);
        }       
    }
}
