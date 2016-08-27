using System;
using System.Runtime.Serialization;

namespace RegApplPortal.Entities.Core.Exceptions
{
    [DataContract]
    public class DataObjectNotFoundException : ApplicationException
    {
        public DataObjectNotFoundException()
        {
        }

        public DataObjectNotFoundException(string message)
            : base(message)
        {
        }

        public DataObjectNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
