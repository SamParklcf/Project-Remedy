namespace Remedy.Core
{
    ///<inheritdoc cref="IDisposable"/>
    public interface IRemedyDisposer : IDisposable, IAsyncDisposable
    {
        /// <summary>
        /// Tracks whether <see cref="IDisposable.Dispose"/> or <see
        /// cref="IAsyncDisposable.DisposeAsync"/> methods has been called or not.
        /// </summary>
        bool Disposed { get; }

        /// <summary> Tracks whether managed resources disposed or not. </summary>
        bool ManagedDisposed { get; }

        /// <summary> Tracks whether unmanaged resources disposed or not. </summary>
        bool UnManagedDisposed { get; }
    }
}