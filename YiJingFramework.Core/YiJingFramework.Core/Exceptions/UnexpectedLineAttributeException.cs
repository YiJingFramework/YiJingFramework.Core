using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiJingFramework.Core.Exceptions
{
    /// <summary>
    /// 当方法接收到一个既不是 <see cref="LineAttribute.Yin"/> 也不是 <see cref="LineAttribute.Yang"/> 的 <see cref="LineAttribute"/> 时，可能被抛出。
    /// Exception that occurs when a method received a <see cref="LineAttribute"/> whose value is neither <see cref="LineAttribute.Yin"/> nor <see cref="LineAttribute.Yang"/>. 
    /// </summary>
    [Serializable]
    public class UnexpectedLineAttributeException : Exception
    {
        /// <summary>
        /// 接收到的值。
        /// The value received.
        /// </summary>
        public LineAttribute ReceivedValue { get; }
        /// <summary>
        /// 初始化一个新实例。
        /// Initialize a new instance.
        /// </summary>
        /// <param name="receivedValue">
        /// 接收到的值。
        /// The value received.
        /// </param>
        public UnexpectedLineAttributeException(LineAttribute receivedValue)
            : this(receivedValue, $"Unexpected Line Attribute: {receivedValue}.") { }

        /// <summary>
        /// 初始化一个新实例。
        /// Initialize a new instance.
        /// </summary>
        /// <param name="receivedValue">
        /// 接收到的值。
        /// The value received.
        /// </param>
        /// <param name="message">
        /// 异常消息。
        /// The message.
        /// </param>
        /// <param name="inner">
        /// 内部异常。
        /// The inner exception.
        /// </param>
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