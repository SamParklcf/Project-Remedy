namespace Remedy.Test.Base
{
    using Remedy.Core;

    public static partial class Expectations
    {
        /// <summary> Checks <paramref name="action"/> for specific exception expectation. </summary>
        /// <param name="action"> Action that should throws exception </param>
        /// <param name="because"> Reason message that why expected exception should be thrown. </param>
        /// <typeparam name="TException"> Type of expected exception. </typeparam>
        /// <returns>
        /// A class Contains a number of methods to assert that an <see cref="Exception"/> is in the
        /// correct state.
        /// </returns>
        /// <exception cref="AssertionFailedException">
        /// Thrown if <paramref name="action"/> is not provided.
        /// </exception>
        public static ExceptionAssertions<TException> Expect<TException>(this Action action, string because = "")
            where TException : Exception
        {
            return action is null
                ? throw new AssertionFailedException($"{nameof(action)} is not provided, could not complete test.")
                : action.Should()
                         .Throw<TException>(because);
        }

        /// <summary>
        /// Checks <paramref name="action"/> for <see cref="ArgumentException"/> expectation.
        /// </summary>
        /// <param name="action"> Action that should throws exception </param>
        /// <param name="because"> Reason message that why expected exception should be thrown. </param>
        /// <returns>
        /// A class Contains a number of methods to assert that an <see cref="Exception"/> is in the
        /// correct state.
        /// </returns>
        public static ExceptionAssertions<ArgumentException> ExpectArgumentException(this Action action, string because = "") =>
            Expect<ArgumentException>(action, because);

        /// <summary>
        /// Checks <paramref name="action"/> for <see cref="ArgumentNullException"/> expectation.
        /// </summary>
        /// <param name="action"> Action that should throws exception </param>
        /// <param name="because"> Reason message that why expected exception should be thrown. </param>
        /// <returns>
        /// A class Contains a number of methods to assert that an <see cref="Exception"/> is in the
        /// correct state.
        /// </returns>
        public static ExceptionAssertions<ArgumentNullException> ExpectArgumentNullException(this Action action, string because = "") =>
            Expect<ArgumentNullException>(action, because);

        /// <summary>
        /// Checks <paramref name="action"/> for s <see cref="InvalidCastException"/> expectation.
        /// </summary>
        /// <param name="action"> Action that should throws exception </param>
        /// <param name="because"> Reason message that why expected exception should be thrown. </param>
        /// <returns>
        /// A class Contains a number of methods to assert that an <see cref="Exception"/> is in the
        /// correct state.
        /// </returns>
        public static ExceptionAssertions<InvalidCastException> ExpectInvalidCastException(this Action action, string because = "") =>
            Expect<InvalidCastException>(action, because);

        /// <summary>
        /// Checks <paramref name="action"/> for <see cref="InvalidOperationException"/> expectation.
        /// </summary>
        /// <param name="action"> Action that should throws exception </param>
        /// <param name="because"> Reason message that why expected exception should be thrown. </param>
        /// <returns>
        /// A class Contains a number of methods to assert that an <see cref="Exception"/> is in the
        /// correct state.
        /// </returns>
        public static ExceptionAssertions<InvalidOperationException> ExpectInvalidOperationException(this Action action, string because = "") =>
            Expect<InvalidOperationException>(action, because);

        /// <summary>
        /// Checks <paramref name="action"/> for <see cref="RemedyInitializationException"/> expectation.
        /// </summary>
        /// <param name="action"> Action that should throws exception </param>
        /// <param name="because"> Reason message that why expected exception should be thrown. </param>
        /// <returns>
        /// A class Contains a number of methods to assert that an <see cref="Exception"/> is in the
        /// correct state.
        /// </returns>
        public static ExceptionAssertions<RemedyInitializationException> ExpectRemedyInitializationException(this Action action, string because = "") =>
            Expect<RemedyInitializationException>(action, because);
    }
}