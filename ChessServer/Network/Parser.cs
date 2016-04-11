using ChessServer.Interfaces;
using Log;
using Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ChessServer.Network
{
    class Parser : IParser
    {
        public Parser()
        {
            formatter = new BinaryFormatter();
        }

        private BinaryFormatter formatter;
        private IServerFacade serverFacade;
        private Type serverFacadeType;

        public void SetFacade(IServerFacade serverFacade)
        {
            this.serverFacade = serverFacade;
            this.serverFacadeType = serverFacade.GetType();
        }
        public void Parse(byte[] msg, int id)
        {
            Message mesg = null;
            try
            {
                using (var stream = new MemoryStream(msg))
                {
                    mesg = formatter.Deserialize(stream) as Message;

                    var args = new object[mesg.Arguments.Length + 1];
                    mesg.Arguments.CopyTo(args, 0);
                    args[args.Length - 1] = id;

                    serverFacadeType.GetMethod(mesg.Method).Invoke(serverFacade, args);
                }
            }
            catch(Exception e)
            {
                string logMsg;

                if (mesg != null)
                {
                    logMsg = "Произошла ошибка при обработке сообщения " + mesg.Method + "\n\r : " + e.Message;
                }
                else
                {
                    logMsg = "Произошла ошибка при десериализации сообщения";
                }

                Logger.Instance.Log( LogLevel.Error, logMsg);
            }
        }


    }
}
