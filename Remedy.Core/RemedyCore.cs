namespace Remedy.Core
{
    using Microsoft.Extensions.DependencyInjection;

    /// <summary> Represents a class for initializing <see cref="Remedy.Core"/> library. </summary>
    public static class RemedyCore
    {
        private static IServiceProvider _serviceProvider;

        /// <summary> Determines if <see cref="Remedy.Core"/> is initialized or not. </summary>
        public static bool CoreInitialized => _serviceProvider is not null;

        private static IServiceProvider ServiceProvider
        {
            get
            {
                return _serviceProvider is null
                    ? throw new RemedyInitializationException($"'{nameof(RemedyCore)}' is not initialized," +
                        $" try use {nameof(RemedyCore)}.{nameof(RemedyCore.Init)} first.")
                    : _serviceProvider;
            }
        }

        /// <summary> Creates a new instance <see cref="IRemedyLifeTracker"/> registered object. </summary>
        /// <returns> </returns>
        public static IRemedyLifeTracker CreateNewLifeTracker() =>
            ServiceProvider.GetService<IRemedyLifeTracker>();

        /// <summary> Initializes <see cref="Remedy.Core"/> library. </summary>
        /// <param name="lifeTracker"> </param>
        public static void Init(IRemedyLifeTracker lifeTracker = null)
        {
            var services = new ServiceCollection();

            if (lifeTracker is null)
                services.AddTransient<IRemedyLifeTracker, RemedyLifeTracker>();
            else
                services.AddTransient(typeof(IRemedyLifeTracker), lifeTracker.GetType());

            _serviceProvider = services.BuildServiceProvider(
                new ServiceProviderOptions()
                {
                    ValidateOnBuild = true
                });
        }
    }
}