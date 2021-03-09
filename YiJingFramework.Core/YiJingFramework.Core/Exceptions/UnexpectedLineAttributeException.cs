using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiJingFramework.Core.Exceptions
{
    [Serializable]
    public class UnexpectedLineAttributeException : Exception
    {
        public LineAttribute ReceivedValue { get; }
        public UnexpectedLineAttributeException(LineAttribute receivedValue)
            : this(receivedValue, $"Unexpected Line Attribute: {receivedValue}.") { }

        public UnexpectedLineAttributeException(
            LineAttribute receivedValue,
            string? message = null,
            Exception? inner = null)
            : base(message, inner)
        {
            this.ReceivedValue = receivedValue;
        }

        protected UnexpectedLineAttributeException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}