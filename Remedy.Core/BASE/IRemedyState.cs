namespace Remedy.Core
{
    /// <summary>
    /// Provides functionalities to fetch state of the object by enumerating throw it's members.
    /// </summary>
    public interface IRemedyState
    {
        /// <summary> Enumerates parents chan og the object. </summary>
        /// <returns> List of objects parents. </returns>
        IReadOnlyList<string> EnumerateParents();

        /// <summary> Gets parents chain of the object. </summary>
        /// <returns> Chain of the object parents </returns>
        string GetParents();

        /// <summary> Gets states of the object. </summary>
        /// <returns> List of object's parameters. </returns>
        IReadOnlyList<RemedyParameter> GetState();

        /// <summary>
        /// Collects <see cref="GetState"/> of the object and packs it to a string format value.
        /// </summary>
        /// <returns> String value represents <see cref="GetState"/> of the object. </returns>
        string Pack();
    }
}