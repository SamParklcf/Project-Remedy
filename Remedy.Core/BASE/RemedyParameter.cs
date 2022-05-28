namespace Remedy.Core
{
    using Remedy.Extensions.Base;
    using Remedy.Extensions.Base.TypeCheckers;

    /// <summary> Represents a class to parameterizing objects. </summary>
    public class RemedyParameter : RemedyNotifier
    {
        private readonly string _name;
        private readonly object _value;

        /// <summary> Initializes a new instance of <see cref="RemedyParameter"/> class. </summary>
        /// <param name="name"> Parameter name. </param>
        /// <param name="value"> Parameter value. </param>
        /// <exception cref="ArgumentNullException">
        /// Throws if <paramref name="name"/> or <paramref name="value"/> be null or empty.
        /// </exception>
        public RemedyParameter(string name, object value)
        {
            Name = name.GetValueIfNotNullOrWhitespace();

            Value = value.GetValueIfNotNull();
        }

        /// <summary> Initializes a new instance of <see cref="RemedyParameter"/> class. </summary>
        protected RemedyParameter()
        {
        }

        /// <summary> Gets or sets the parameter name. </summary>
        public string Name
        {
            get => _name;
            protected init => SetProperty(ref _name, value);
        }

        /// <summary> Gets or sets the parameter value. </summary>
        public object Value
        {
            get => _value;
            protected init => SetProperty(ref _value, value);
        }

        ///<inheritdoc/>
        public override bool Equals(object obj) =>
            obj.Equals<RemedyParameter>(x =>
                x.Name == Name &&
                x.Value.Equals<object>(Value));

        ///<inheritdoc/>
        public override int GetHashCode() =>
            Math.Abs(Tuple.Create(Name, Value).GetHashCode());

        ///<inheritdoc/>
        public override string ToString() =>
            $"{Name ?? "?"} : {Value ?? "?"}";
    }

    /// <summary> Represents a class to parameterizing objects. </summary>
    /// <typeparam name="TValue"> Type of the <see cref="Value"/>. </typeparam>
    public class RemedyParameter<TValue> : RemedyParameter
    {
        private readonly TValue _value;

        /// <summary> Initializes a new instance of <see cref="RemedyParameter"/> class. </summary>
        /// <param name="name"> Parameter name. </param>
        /// <param name="value"> Parameter value. </param>
        /// <exception cref="ArgumentNullException">
        /// Throws if <paramref name="name"/> or <paramref name="value"/> be null or empty.
        /// </exception>
        public RemedyParameter(string name, TValue value)
        {
            Name = name.GetValueIfNotNullOrWhitespace();

            Value = value.GetValueIfNotNull();
        }

        /// <summary> Initializes a new instance of <see cref="RemedyParameter"/> class. </summary>
        protected RemedyParameter() : base()
        {
        }

        /// <summary> Gets or sets the paramter value. </summary>
        public new TValue Value
        {
            get => _value;
            init => SetProperty(ref _value, value);
        }

        ///<inheritdoc/>
        public override bool Equals(object obj) =>
            obj.Equals<RemedyParameter<TValue>>(x =>
                x.Name == Name &&
                x.Value.Equals<TValue>(Value));

        ///<inheritdoc/>
        public override int GetHashCode() =>
            Math.Abs(Tuple.Create(Name, Value).GetHashCode());
    }
}