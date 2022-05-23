namespace Remedy.Core
{
    using System.Runtime.Serialization;

    /// <summary> Represents errors that occur during application execution. </summary>
    public class RemedyException : Exception
    {
        /// <summary> Initializes a new instance of <see cref="RemedyException"/>. </summary>
        public RemedyException()
        {
        }

        /// <summary> Initializes a new instance of <see cref="RemedyException"/> class. </summary>
        /// <param name="message"> The message that describes the error. </param>
        public RemedyException(string message) : base(message)
        {
        }

        /// <summary> Initializes a new instance of <see cref="RemedyException"/> class. </summary>
        /// <param name="message"> The message that describes the error. </param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference (Nothing
        /// in Visual Basic) if no inner exception is specified.
        /// </param>
        public RemedyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary> Initializes a new instance of <see cref="RemedyException"/> class. </summary>
        /// <param name="info">
        /// The System.Runtime.Serialization.SerializationInfo that holds the serialized object data
        /// about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The System.Runtime.Serialization.StreamingContext that contains contextual information
        /// about the source or destination.
        /// </param>
        protected RemedyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}