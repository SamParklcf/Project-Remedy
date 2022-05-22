namespace Remedy.Test.Base
{
    using System.Runtime.Serialization;

    /// <summary> Represents errors that occur during assertions execution. </summary>
    public class AssertionFailedException : Exception
    {
        /// <summary> Initializes a new instance of <see cref="AssertionFailedException"/> class. </summary>
        public AssertionFailedException()
        {
        }

        /// <summary> Initializes a new instance of <see cref="AssertionFailedException"/> class. </summary>
        /// <param name="message"> The message that describes the error. </param>
        public AssertionFailedException(string message) : base(message)
        {
        }

        /// <summary> Initializes a new instance of <see cref="AssertionFailedException"/> class. </summary>
        /// <param name="message"> The message that describes the error. </param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference (Nothing
        /// in Visual Basic) if no inner exception is specified.
        /// </param>
        public AssertionFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary> Initializes a new instance of <see cref="AssertionFailedException"/> class. </summary>
        /// <param name="info">
        /// The System.Runtime.Serialization.SerializationInfo that holds the serialized object data
        /// about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The System.Runtime.Serialization.StreamingContext that contains contextual information
        /// about the source or destination.
        /// </param>
        protected AssertionFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}