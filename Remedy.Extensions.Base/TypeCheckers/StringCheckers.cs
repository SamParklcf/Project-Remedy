namespace Remedy.Extensions.Base.TypeCheckers
{
    /// <summary> Provides functionalities for null checking of any strings. </summary>
    public static class StringCheckers
    {
        /// <summary>
        /// Checks <paramref name="input"/> to be not null or empty. otherwise it throws
        /// <typeparamref name="TException"/>.
        /// </summary>
        /// <typeparam name="TException"> Type of exception to be thrown. </typeparam>
        /// <param name="input"> Input to be checked. </param>
        /// <param name="exception"> Exception to be thrown. </param>
        public static void EnsureNotNullOrEmpty<TException>(this string input, TException exception)
            where TException : notnull, Exception
        {
            if (string.IsNullOrEmpty(input))
                throw exception;
        }

        /// <summary>
        /// Checks <paramref name="input"/> to be not null or empty. otherwise it throws <see cref="ArgumentNullException"/>.
        /// </summary>
        /// <param name="input"> Input to be checked. </param>
        /// <param name="message"> Custom message to be injected into <see cref="ArgumentNullException"/>. </param>
        public static void EnsureNotNullOrEmpty(this string input, string message = "") =>
            EnsureNotNullOrEmpty(input,
                new ArgumentNullException(nameof(input), message));

        /// <summary>
        /// Checks <paramref name="input"/> to be not null or whitespace. otherwise it throws
        /// <typeparamref name="TException"/>.
        /// </summary>
        /// <typeparam name="TException"> Type of exception to be thrown. </typeparam>
        /// <param name="input"> Input to be checked. </param>
        /// <param name="exception"> Exception to be thrown. </param>
        public static void EnsureNotNullOrWhitespace<TException>(this string input, TException exception)
            where TException : notnull, Exception
        {
            if (string.IsNullOrWhiteSpace(input))
                throw exception;
        }

        /// <summary>
        /// Checks <paramref name="input"/> to be not null or whitespace. otherwise it throws <see cref="ArgumentNullException"/>.
        /// </summary>
        /// <param name="input"> Input to be checked. </param>
        /// <param name="message"> Custom message to be injected into <see cref="ArgumentNullException"/>. </param>
        public static void EnsureNotNullOrWhitespace(this string input, string message = "") =>
            EnsureNotNullOrWhitespace(input,
                new ArgumentNullException(nameof(input), message));

        /// <summary>
        /// Gets <paramref name="input"/> to be not null or empty. otherwise it throws <see cref="ArgumentNullException"/>.
        /// </summary>
        /// <param name="input"> Input to be checked. </param>
        /// <param name="message"> Custom message to be injected into <see cref="ArgumentNullException"/>. </param>
        /// <returns> <paramref name="input"/>. </returns>
        public static string GetValueIfNotNullOrEmpty(this string input, string message = "") =>
            GetValueIfNotNullOrEmpty(input, new ArgumentNullException(message));

        /// <summary>
        /// Gets <paramref name="input"/> to be not null or empty. otherwise it throws <typeparamref name="TException"/>.
        /// </summary>
        /// <typeparam name="TException"> Type of exception to be thrown. </typeparam>
        /// <param name="input"> Input to be checked. </param>
        /// <param name="exception"> Exception to be thrown. </param>
        public static string GetValueIfNotNullOrEmpty<TException>(this string input, TException exception)
            where TException : notnull, Exception
        {
            EnsureNotNullOrEmpty(input, exception);

            return input;
        }

        /// <summary>
        /// Gets <paramref name="input"/> to be not null or whitespace. otherwise it throws <see cref="ArgumentNullException"/>.
        /// </summary>
        /// <param name="input"> Input to be checked. </param>
        /// <param name="message"> Custom message to be injected into <see cref="ArgumentNullException"/>. </param>
        /// <returns> <paramref name="input"/>. </returns>
        public static string GetValueIfNotNullOrWhitespace(this string input, string message = "") =>
            GetValueIfNotNullOrWhitespace(input, new ArgumentNullException(message));

        /// <summary>
        /// Gets <paramref name="input"/> to be not null or whitespace. otherwise it throws
        /// <typeparamref name="TException"/>.
        /// </summary>
        /// <typeparam name="TException"> Type of exception to be thrown. </typeparam>
        /// <param name="input"> Input to be checked. </param>
        /// <param name="exception"> Exception to be thrown. </param>
        public static string GetValueIfNotNullOrWhitespace<TException>(this string input, TException exception)
            where TException : notnull, Exception
        {
            EnsureNotNullOrWhitespace(input, exception);

            return input;
        }
    }
}