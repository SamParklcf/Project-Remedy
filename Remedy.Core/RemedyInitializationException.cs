namespace Remedy.Core
{
    using System;

    /// <summary> Represents errors that occur during application initialization. </summary>
    public class RemedyInitializationException : RemedyException
    {
        /// <summary>
        /// Initializes a new instance of <see cref="RemedyInitializationException"/> class.
        /// </summary>
        /// <param name="message"> The message that describes the error. </param>
        public RemedyInitializationException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="RemedyInitializationException"/> class.
        /// </summary>
        /// <param name="message"> The message that describes the error. </param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference (Nothing
        /// in Visual Basic) if no inner exception is specified.
        /// </param>
        public RemedyInitializationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}