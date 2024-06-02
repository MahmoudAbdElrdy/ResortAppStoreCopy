using Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extensions
{
    [Serializable]
    public class UserFriendlyException : BaseException
    {
        public UserFriendlyException()
        {

        }

        public UserFriendlyException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        public UserFriendlyException(string message)
            : base(message)
        {

        }

        public UserFriendlyException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
