using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Log
{
    public class Logger : ILogger
    {
        public Logger()
        {
            buffer = new StringBuilder(bufferSize);
        }

        private static Lazy<Logger> _instance = new Lazy<Logger>(LazyThreadSafetyMode.PublicationOnly);
        public static ILogger Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        private string logFile;
        private StringBuilder buffer;
        private int bufferSize = 100;

        public void SetLogFile(string path)
        {
            logFile = path;
        }
        public void Log(LogLevel level, string msg)
        {
            buffer.Append(
                DateTime.Now.ToString() + " " + level.ToString() + ": " + msg + "\n\r"
                );

            //if (buffer.Length > bufferSize)
           // {
                Flush();
                buffer.Clear();
           // }
        }
        public void Flush()
        {
            using (var stream = new StreamWriter(logFile))
            {
                stream.WriteLine(buffer.ToString());
            }
        }
    }
}
