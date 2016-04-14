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
            lck = new object();
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
        private object lck;

        public void SetLogFile(string path)
        {
            logFile = path;
            using (File.Create(path)) { }
            
        }
        public void Log(LogLevel level, string msg)
        {
            lock (lck)
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
        }
        public void Flush()
        {
            FileStream stream = new FileStream(logFile, FileMode.Append);
            using(var writer = new StreamWriter(stream))
            {
                writer.WriteLine(buffer.ToString());
            }
        }
    }
}
