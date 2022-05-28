namespace Remedy.Extensions.Base.TypeCheckers
{
    /// <summary> Provides functionalities for checking of any guids. </summary>
    public static class GuidCheckers
    {
        /// <summary>
        /// Checks input to be not <see cref="Guid"/>. <see cref="Guid.Empty"/>. otherwise it throws
        /// <typeparamref name="TException"/>.
        /// </summary>
        /// <typeparam name="TException"> Type of onEmptyException to be thrown. </typeparam>
        /// <param name="input"> Input to be checked. </param>
        /// <param name="onEmptyException"> Exception to be thrown. </param>
        public static void EnsureNotEmpty<TException>(this Guid input, TException onEmptyException)
            where TException : notnull, Exception
        {
            if (input == Guid.Empty)
                throw onEmptyException;
        }

        /// <summary>
        /// Checks input to be not <see cref="Guid"/>. <see cref="Guid.Empty"/>. otherwise it throws
        /// <see cref="ArgumentException"/>.
        /// </summary>
        /// <param name="input"> Input to be checked. </param>
        /// <param name="onEmptyMessage">
        /// Custom onEmptyMessage to be injected into <see cref="ArgumentException"/>. uses default
        /// onEmptyMessage if not provided.
        /// </param>
        public static void EnsureNotEmpty(this Guid input, string onEmptyMessage = "") =>
            EnsureNotEmpty(input,
                new ArgumentException(
                    string.IsNullOrWhiteSpace(onEmptyMessage) ? $"{nameof(input)} should not be {nameof(Guid)}.{nameof(Guid.Empty)}" : onEmptyMessage));

        /// <summary>
        /// Checks <paramref name="input"/> to be not null or whitespace. otherwise it throws
        /// <typeparamref name="TException"/>.
        /// </summary>
        /// <typeparam name="TException"> Type of onEmptyException to be thrown. </typeparam>
        /// <param name="input"> Input to be checked. </param>
        /// <param name="onEmptyException"> Exception to be thrown. </param>
        /// <returns> <paramref name="input"/>. </returns>
        public static Guid GetValueIfNotEmpty<TException>(this Guid input, TException onEmptyException)
            where TException : notnull, Exception
        {
            EnsureNotEmpty(input, onEmptyException);

            return input;
        }

        /// <summary>
        /// Checks <paramref name="input"/> to be not null or whitespace. otherwise it throws <see cref="ArgumentException"/>.
        /// </summary>
        /// <param name="input"> Input to be checked. </param>
        /// <param name="onEmptyMessage"> Custom onEmptyMessage to be injected into <see cref="ArgumentException"/>. </param>
        /// <returns> <paramref name="input"/>. </returns>
        public static Guid GetValueIfNotEmpty(this Guid input, string onEmptyMessage = "")
        {
            EnsureNotEmpty(input, onEmptyMessage);

            return input;
        }
    }
}