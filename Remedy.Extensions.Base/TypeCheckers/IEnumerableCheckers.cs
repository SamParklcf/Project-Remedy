namespace Remedy.Extensions.Base.TypeCheckers
{
    using System.Collections;
    /// <summary> Provides functionalities for null checking of any ienumerables. </summary>
    public static class IEnumerableCheckers
    {
        /// <summary>
        /// Checks <paramref name="items"/> to be not null and have count of <paramref
        /// name="count"/>. otherwise it throws <typeparamref name="TException"/>.
        /// </summary>
        /// <typeparam name="TException"> Type of exception to be thrown. </typeparam>
        /// <param name="items"> Items to be checked. </param>
        /// <param name="count"> Count of <paramref name="items"/> to be considered. </param>
        /// <param name="exception"> Exception to be thrown. </param>
        public static void EnsureHaveCount<TException>(this IEnumerable items, int count, TException exception)
                    where TException : notnull, Exception
        {
            items.EnsureNotNull(exception);

            var itemsCount = 0;
            var enumerator = items.GetEnumerator();
            while (enumerator.MoveNext())
            {
                itemsCount++;
                if (itemsCount == count)
                    break;
            }

            if (itemsCount != count)
                throw exception;
        }

        /// <summary>
        /// Checks <paramref name="items"/> to be not null and have count of <paramref
        /// name="count"/>. otherwise it throws <see cref="ArgumentException"/>.
        /// </summary>
        /// <param name="items"> Items to be checked. </param>
        /// <param name="count"> Count of <paramref name="items"/> to be considered. </param>
        /// <param name="message">
        /// Custom message to be injected into <see cref="ArgumentException"/>. uses default message
        /// if not provided.
        /// </param>
        public static void EnsureHaveCount(this IEnumerable items, int count, string message = "") =>
            EnsureHaveCount(items, count,
                new ArgumentException(
                    string.IsNullOrWhiteSpace(message) ? $"{nameof(items)} has not '{count}' element(s)" : message));
    }
}