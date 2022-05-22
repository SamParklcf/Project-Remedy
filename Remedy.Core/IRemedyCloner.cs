namespace Remedy.Core
{
    ///<inheritdoc cref="ICloneable"/>
    public interface IRemedyCloner : ICloneable
    {
        /// <summary> Creates a new object that is a copy of the current instance. </summary>
        /// <typeparam name="T"> Type which cloned object be casted. </typeparam>
        /// <returns> A new object that is a copy of this instance. </returns>
        T Clone<T>();

        /// <summary> Creates a new object that is a Deep-copy of the current instance. </summary>
        /// <returns> A new object that is a Deep-copy of this instance. </returns>
        object DeepClone();

        /// <summary> Creates a new object that is a Deep-copy of the current instance. </summary>
        /// <typeparam name="T"> Type which cloned object be casted. </typeparam>
        /// <returns> A new object that is a Deep-copy of this instance. </returns>
        T DeepClone<T>();
    }
}