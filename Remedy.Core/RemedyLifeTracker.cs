namespace Remedy.Core
{
    using Remedy.Core.Extensions;
    using Remedy.Core.Extensions.Reflection;

    /// <summary> Represents a class for object life time tracking. </summary>
    public class RemedyLifeTracker : RemedyNotifier, IRemedyLifeTracker
    {
        /// <summary> Initializes a new instance of <see cref="RemedyLifeTracker"/> class. </summary>
        public RemedyLifeTracker()
        {
            BirthTime = DateTime.Now;
            DeathTime = DateTime.MinValue;
        }

        /// <summary> Gets or sets object birth time. </summary>
        protected DateTime BirthTime { get; set; }

        /// <summary> Gets or set object death time (disposed time). </summary>
        protected DateTime DeathTime { get; set; }

        ///<inheritdoc/>
        public IReadOnlyList<string> EnumerateParents() =>
                   GetType().EnumerateParents();

        ///<inheritdoc/>
        public override bool Equals(object obj) =>
            obj.Equals<RemedyLifeTracker>(
                x =>
                    x.GetALiveTime() == GetALiveTime());

        ///<inheritdoc/>
        public TimeSpan GetALiveTime() =>
                    DeathTime == DateTime.MinValue ? DateTime.Now - BirthTime : DeathTime - BirthTime;

        ///<inheritdoc/>
        public DateTime GetBirthTime() =>
            BirthTime;

        ///<inheritdoc/>
        public DateTime GetDeathTime() =>
             DeathTime;

        ///<inheritdoc/>
        public override int GetHashCode() =>
            Math.Abs(Tuple.Create(BirthTime, DeathTime).GetHashCode());

        ///<inheritdoc/>
        public string GetParents() =>
                    GetType().GetParents();

        ///<inheritdoc/>
        public IReadOnlyList<RemedyParameter> GetState() =>
            new List<RemedyParameter>
                   {
                       new RemedyParameter<DateTime>(nameof(BirthTime), BirthTime),
                       new RemedyParameter<DateTime>(nameof(DeathTime), DeathTime),
                       new RemedyParameter<TimeSpan>("ALiveTime", GetALiveTime()),
                       new RemedyParameter<bool>(nameof(IsObjectAlive), IsObjectAlive())
                   };

        ///<inheritdoc/>
        public bool IsObjectAlive()
        {
            return DeathTime == DateTime.MinValue && BirthTime != DateTime.MinValue;
        }

        ///<inheritdoc/>
        public DateTime RegisterDeath()
        {
            if (DeathTime == DateTime.MinValue)
            {
                DeathTime = DateTime.Now;
            }

            return DeathTime;
        }

        ///<inheritdoc/>
        public override string ToString() =>
            $"[IsAlive: {IsObjectAlive()}] - [Duration: {GetALiveTime()}]";
    }
}