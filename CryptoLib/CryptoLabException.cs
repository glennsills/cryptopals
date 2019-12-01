using System;
using System.Runtime.Serialization;

namespace CryptoLib
{
    [Serializable]
    internal class CryptoLabException : Exception
    {
        public CryptoLabException()
        {
        }

        public CryptoLabException(string message) : base(message)
        {
        }

        public CryptoLabException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CryptoLabException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}