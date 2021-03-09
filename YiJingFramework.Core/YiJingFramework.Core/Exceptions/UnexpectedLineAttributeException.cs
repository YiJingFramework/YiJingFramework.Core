using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiJingFramework.Core.Exceptions
{
    /// <summary>
    /// Exception that occurs when receiving a <see cref="LineAttribute"/> whose value is neither <see cref="LineAttribute.Yin"/> nor <see cref="LineAttribute.Yang"/>. 
    /// </summary>
    [Serializable]
    public class UnexpectedLineAttributeException : Exception
    {
        /// <summary>
        /// The value received.
        /// </summary>
        public LineAttribute ReceivedValue { get; }
        /// <summary>
        /// Initialize a <see cref="UnexpectedLineAttributeException"/>.
        /// </summary>
        /// <param name="receivedValue">The value received.</param>
        public UnexpectedLineAttributeException(LineAttribute receivedValue)
            : this(receivedValue, $"Unexpected Line Attribute: {receivedValue}.") { }
        /// <summary>
        /// Initialize a <see cref="UnexpectedLineAttributeException"/>.
        /// </summary>
        /// <param name="receivedValue">The value received.</param>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner exception.</param>
        public UnexpectedLineAttributeException(
            LineAttribute receivedValue,
            string? message = null,
            Exception? inner = null)
            : base(message, inner)
        {
            this.ReceivedValue = receivedValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected UnexpectedLineAttributeException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}