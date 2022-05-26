namespace Remedy.Extensions.Base
{
    /// <summary> Provides extensions to checking equality. </summary>

    public static class EqualityCheckers
    {
        /// <summary> Determines whether the specified object is equal to the current object. </summary>
        /// <param name="input"> Object #1 to be checked. </param>
        /// <param name="equalsTo"> Func to use in custom checks. </param>
        /// <typeparam name="TExpected"> Type of object #2 to be compared with <paramref name="input"/>. </typeparam>
        /// <returns>
        /// True if <paramref name="input"/> is not null and it's type equals to <typeparamref
        /// name="TExpected"/> and <paramref name="equalsTo"/> returns true, otherwise false.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="equalsTo"/> func is not provided.
        /// </exception>
        public static bool Equals<TExpected>(this object input, Func<TExpected, bool> equalsTo) =>
            equalsTo is null
                ? throw new ArgumentNullException(nameof(equalsTo))
                : input is not null &&
                  input is TExpected expected &&
                  equalsTo(expected);

        /// <summary> Determines whether the specified object is equal to the current object. </summary>
        /// <typeparam name="TExpected">
        /// Type of object #2 to be compared with <paramref name="input"/>.
        /// </typeparam>
        /// <param name="input"> Object #1 to be checked. </param>
        /// <param name="expected"> Object #2 to be compared with <paramref name="input"/>. </param>
        /// <returns> </returns>
        public static bool Equals<TExpected>(this object input, TExpected expected)
        {
            return (input is null && expected is null) ||
                ((input is not null || expected is null) && (input is null || expected is not null) &&
                input is TExpected expectedInput &&
                EqualityComparer<TExpected>.Default.Equals(expectedInput, expected));
        }

        /// <summary> Checks if <paramref name="input"/> is of type <typeparamref name="TExpected"/>. </summary>
        /// <typeparam name="TExpected"> Type to be checked for matching. </typeparam>
        /// <param name="input"> Input to be checked. </param>
        /// <returns>
        /// if type matched, New instance of <typeparamref name="TExpected"/>, otherwise default of
        /// <typeparamref name="TExpected"/>.
        /// </returns>
        public static bool OfType<TExpected>(this object input) =>
            input is TExpected;
    }
}