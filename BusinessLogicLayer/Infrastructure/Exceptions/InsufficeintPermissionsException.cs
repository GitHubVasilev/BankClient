using System;

namespace BusinessLogicLayer.Infrastructure.Exceptions
{

    [Serializable]
    public class InsufficeintPermissionsException : Exception
    {
        public InsufficeintPermissionsException() { }
        public InsufficeintPermissionsException(string message) : base(message) { }
        public InsufficeintPermissionsException(string message, Exception inner) : base(message, inner) { }
        protected InsufficeintPermissionsException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
