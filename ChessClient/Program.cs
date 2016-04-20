using ChessClient.Controllers;
using ChessClient.Interfaces;
using ChessClient.Network;
using Log;
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

            Logger.Instance.SetLogFile("clientlog.txt");

            var mainForm = new MainForm();
            var serverProxy = new ServerProxy();               
            var screenManager = new ScreenManager(mainForm,  serverProxy);
            var clientFacade = new ClientFacade(screenManager);
           
            var socketListener = new SocketListener(clientFacade);

            serverProxy.Init(clientFacade, socketListener);
            socketListener.SetParser(serverProxy);
                     
            Application.Run(mainForm);
        }       
    }
}
