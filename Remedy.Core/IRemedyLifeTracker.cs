namespace Remedy.Core
{
    /// <summary> Provides functionalies for object life time tracking. </summary>
    public interface IRemedyLifeTracker : IRemedyState
    {
        /// <summary> Gets total object alive time. </summary>
        /// <returns> </returns>
        TimeSpan GetALiveTime();

        /// <summary> Gets object birth time. </summary>
        /// <returns> </returns>
        DateTime GetBirthTime();

        /// <summary> Gets object death time. </summary>
        /// <returns> </returns>
        DateTime GetDeathTime();

        /// <summary> Determines if object is alive or not. </summary>
        /// <returns> </returns>
        bool IsObjectAlive();

        /// <summary> Registers object death (dispose state). </summary>
        /// <returns> </returns>
        DateTime RegisterDeath();
    }
}