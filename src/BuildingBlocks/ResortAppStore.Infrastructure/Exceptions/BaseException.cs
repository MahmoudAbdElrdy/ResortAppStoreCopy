using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    [Serializable]
    public class BaseException : Exception
    {
        public BaseException()
        {

        }

        public BaseException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        public BaseException(string message)
            : base(message)
        {

        }

        public BaseException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}