namespace Remedy.Core
{
    using Remedy.Core.Validating;
    using Remedy.Extensions.Base.TypeCheckers;

    /// <summary> Represents a base class to make an object. </summary>
    public class RemedyObject : RemedyModel, IRemedyDisposer, IRemedyLifeTracker
    {
        /// <summary> Initializes a new instance of <see cref="RemedyObject"/> class. </summary>
        public RemedyObject()
        {
            LifeTracker = RemedyCore.CreateNewLifeTracker();
        }

        /// <summary> Initializes a new instance of <see cref="RemedyObject"/> class. </summary>
        /// <param name="lifeTracker"> Life time tracker for object. </param>
        /// <exception cref="ArgumentNullException">
        /// Throws if <paramref name="lifeTracker"/> passed by null.
        /// </exception>
        public RemedyObject(IRemedyLifeTracker lifeTracker)
        {
            lifeTracker.EnsureNotNull();

            LifeTracker = lifeTracker;
        }

        /// <summary> Dispose the instance of <see cref="RemedyObject"/> class. </summary>
        ~RemedyObject()
        {
            Dispose(false);
        }

        ///<inheritdoc/>
        public bool Disposed => ManagedDisposed && UnManagedDisposed;

        ///<inheritdoc/>
        public bool ManagedDisposed { get; protected set; }

        ///<inheritdoc/>
        public bool UnManagedDisposed { get; protected set; }

        ///<inheritdoc/>
        protected IRemedyLifeTracker LifeTracker { get; }

        ///<inheritdoc cref="GC.Collect(int, GCCollectionMode, bool, bool)"/>
        public void CollectMyGeneration(GCCollectionMode mode = GCCollectionMode.Default, bool blocking = false, bool compacting = false) =>
             GC.Collect(GetMyGeneration(), mode, blocking, compacting);

        ///<inheritdoc/>
        public void Dispose()
        {
            if (Disposed)
            {
                return;
            }

            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ///<inheritdoc/>
        public async ValueTask DisposeAsync()
        {
            await DisposeManagedAsync();

            ManagedDisposed = true;

            Dispose(false); //just dispose unmanaged, because managed will be disposed by DisposeManagedAsync
            GC.SuppressFinalize(this);
        }

        ///<inheritdoc/>
        public TimeSpan GetALiveTime() =>
                    LifeTracker.GetALiveTime();

        ///<inheritdoc/>
        public DateTime GetBirthTime() =>
                    LifeTracker.GetBirthTime();

        ///<inheritdoc/>
        public DateTime GetDeathTime() =>
                    LifeTracker.GetDeathTime();

        ///<inheritdoc/>
        public int GetMyGeneration() =>
            GC.GetGeneration(this);

        ///<inheritdoc/>
        public bool IsObjectAlive() =>
                            LifeTracker.IsObjectAlive();

        ///<inheritdoc cref="GC.KeepAlive(object?)"/>
        public void KeepMeAlive() =>
            GC.KeepAlive(this);

        ///<inheritdoc/>
        DateTime IRemedyLifeTracker.RegisterDeath()
        {
            throw new NotImplementedException();
        }

        ///<inheritdoc cref="GC.ReRegisterForFinalize(object)"/>
        public void ReRegisterMeForFinalize() =>
            GC.ReRegisterForFinalize(this);

        ///<inheritdoc/>
        public override string Pack() =>
            $"{base.Pack()} - {LifeTracker}";

        ///<inheritdoc/>
        public override bool Validate()
        {
            return base.Validate() && Validate(new RemedyObjectValidator()).IsValid;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// managed resources. see <see href="https://docs.microsoft.com/en-us/dotnet/api/system.idisposable?view=net-5.0"/>
        /// </summary>
        protected virtual void DisposeManaged()
        {
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// managed resources asynchronously. <see href="https://docs.microsoft.com/en-us/dotnet/standard/garbage-collection/implementing-disposeasync"/>
        /// </summary>
        /// <returns> A task that represents the asynchronous dispose operation. </returns>
        protected virtual async ValueTask DisposeManagedAsync()
        {
            //More info about ConfigureAwait: https://www.youtube.com/watch?v=F9_8MJbsnzg&t=332s
            await Task.CompletedTask.ConfigureAwait(false);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources. see <see href="https://docs.microsoft.com/en-us/dotnet/api/system.idisposable?view=net-5.0"/>
        /// </summary>
        protected virtual void DisposeUnmanaged()
        {
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources. <see href="https://docs.microsoft.com/en-us/dotnet/api/system.idisposable?view=net-5.0"/>
        /// </summary>
        /// <param name="disposing">
        /// If disposing equals true, dispose all managed and unmanaged resources.
        /// </param>
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                DisposeManaged();

                ManagedDisposed = true;
            }

            DisposeUnmanaged();
            UnManagedDisposed = true;

            LifeTracker.RegisterDeath();
        }
    }

    /// <summary> Represents a class for validating <see cref="RemedyObject"/>. </summary>
    public class RemedyObjectValidator : RemedyValidator<RemedyObject>
    {
        ///<inheritdoc/>
        public override void RegisterRules()
        {
        }
    }
}