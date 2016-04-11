using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log
{
    public interface ILogger
    {
        void SetLogFile(string path);

        void Log(LogLevel level, string msg);
        void Flush();
    }
}
