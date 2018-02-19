using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DalException : Exception
    {
        public DalException()
        {
        }

        public DalException(string message) : base(message)
        {
        }

        public DalException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DalException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
