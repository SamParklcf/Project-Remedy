namespace Remedy.Extensions.Base.TypeCheckers
{
    /// <summary> Provides functionalities for null checking of any classes. </summary>
    public static class TypeCheckers
    {
        /// <summary>
        /// Checks <paramref name="input"/> to be not null. otherwise it throws <typeparamref name="TException"/>.
        /// </summary>
        /// <typeparam name="T"> Type of input to be checked. </typeparam>
        /// <typeparam name="TException"> Type of exception to be thrown. </typeparam>
        /// <param name="input"> Input to be checked. </param>
        /// <param name="exception"> Exception to be thrown. </param>
        public static void EnsureNotNull<T, TException>(this T input, TException exception)
            where TException : notnull, Exception
        {
            if (input is null)
                throw exception;
        }

        /// <summary>
        /// Checks <paramref name="input"/> to be not null. otherwise it throws <see cref="ArgumentNullException"/>.
        /// </summary>
        /// <typeparam name="T"> Type of input to be checked. </typeparam>
        /// <param name="input"> Input to be checked. </param>
        /// <param name="message"> Custom message to be injected into <see cref="ArgumentNullException"/>. </param>
        public static void EnsureNotNull<T>(this T input, string message = "") =>
            EnsureNotNull(input,
                new ArgumentNullException(nameof(input), message));

        /// <summary>
        /// Gets <paramref name="input"/> to be not null. otherwise it throws <typeparamref name="TException"/>.
        /// </summary>
        /// <typeparam name="T"> Type of input to be checked. </typeparam>
        /// <typeparam name="TException"> Type of exception to be thrown. </typeparam>
        /// <param name="input"> Input to be checked. </param>
        /// <param name="exception"> Exception to be thrown. </param>
        /// <returns> <paramref name="input"/>. </returns>
        public static T GetValueIfNotNull<T, TException>(this T input, TException exception)
            where TException : notnull, Exception
        {
            EnsureNotNull(input, exception);

            return input;
        }

        /// <summary>
        /// Gets <paramref name="input"/> to be not null. otherwise it throws <see cref="ArgumentNullException"/>.
        /// </summary>
        /// <typeparam name="T"> Type of input to be checked. </typeparam>
        /// <param name="input"> Input to be checked. </param>
        /// <param name="message"> Custom message to be injected into <see cref="ArgumentNullException"/>. </param>
        /// <returns> <paramref name="input"/>. </returns>
        public static T GetValueIfNotNull<T>(this T input, string message = "")
        {
            EnsureNotNull(input, message);

            return input;
        }
    }
}