namespace Remedy.Extensions.Base.Reflection
{
    /// <summary> Provides extensions to work with type information of parents. </summary>
    public static class ParentalReflectors
    {
        /// <summary> Use this value at the end of parenting enumerating. </summary>
        public const string EndValue = "[END]";

        /// <summary> Enumerates parents chan og the object. </summary>
        /// <param name="type"> Type to be parents fetched. </param>
        /// <returns> List of objects parents. </returns>
        public static IReadOnlyList<string> EnumerateParents(this Type type) =>
                    GetParents(type).Split(" -> ").SkipLast(1).ToList();

        /// <summary> Enumerates parents chan og the object. </summary>
        /// <param name="obj"> Type to be parents fetched. </param>
        /// <returns> List of objects parents. </returns>
        public static IReadOnlyList<string> EnumerateParents(this object obj) =>
                    GetParents(obj).Split(" -> ").SkipLast(1).ToList();

        /// <summary> Gets parents chain of a type. </summary>
        /// <param name="obj"> Type to be parents fetched. </param>
        /// <returns> Chain of parents type. </returns>
        public static string GetParents(this object obj)
        {
            var type = obj?.GetType();
            return type is null || type.BaseType is null ? EndValue : GetParents(obj?.GetType());
        }

        /// <summary> Gets parents chain of a type. </summary>
        /// <param name="type"> Type to be parents fetched. </param>
        /// <returns> Chain of parents type. </returns>
        public static string GetParents(this Type type) =>
            type?.BaseType is null ? EndValue : $"[{type.BaseType.Name}] -> " + GetParents(type.BaseType);
    }
}