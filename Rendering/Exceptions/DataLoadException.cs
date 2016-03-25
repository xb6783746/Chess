using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rendering.Exceptions
{
    [Serializable]
    internal class DataLoadException : ApplicationException
    {
        public DataLoadException() { }
        public DataLoadException(string message) : base("Ошибка загрузки файлов") { }
        public DataLoadException(string message, Exception inner) : base(message, inner) { }
        protected DataLoadException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
