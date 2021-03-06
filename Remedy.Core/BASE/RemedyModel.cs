namespace Remedy.Core
{
    using System.ComponentModel;

    using FluentValidation;
    using FluentValidation.Results;

    using Remedy.Core.Validating;
    using Remedy.Extensions.Base.Reflection;
    using Remedy.Extensions.Base.TypeCheckers;

    /// <summary> Represents a base class for modeling objects. </summary>
    public class RemedyModel : RemedyNotifier, IRemedyState, IRemedyCloner, IRemedyValidator<RemedyModel>
    {
        private Guid _id;
        private bool _isValid;

        /// <summary> Initializes a new instance of <see cref="RemedyModel"/> class. </summary>
        public RemedyModel() : this(Guid.NewGuid())
        {
        }

        /// <summary> Initializes a new instance of <see cref="RemedyModel"/> class. </summary>
        /// <param name="id"> An unique identifier for the object. </param>
        /// <exception cref="ArgumentException"> Throws if <paramref name="id"/> be empty. </exception>
        protected RemedyModel(Guid id)
        {
            Id = id.GetValueIfNotEmpty($"{nameof(id)} is not provided correctly, and should not be empty.");
        }

        /// <summary> Gets an unique identifier for the object. </summary>
        public Guid Id
        {
            get => _id;
            private set => SetProperty(ref _id, value);
        }

        ///<inheritdoc/>
        public bool IsValid
        {
            get => _isValid;
            private set => SetProperty(ref _isValid, value);
        }

        ///<inheritdoc/>
        public BindingList<ValidationFailure> ValidationFailures { get; private set; }

        ///<inheritdoc/>
        public object Clone() => MemberwiseClone();

        ///<inheritdoc/>
        public T Clone<T>() => (T)Clone();

        ///<inheritdoc/>
        public object DeepClone() => PerformDeepClone();

        ///<inheritdoc/>
        public T DeepClone<T>() => (T)DeepClone();

        ///<inheritdoc/>
        public IReadOnlyList<string> EnumerateParents() =>
            GetType().EnumerateParents();

        ///<inheritdoc/>
        public override int GetHashCode() =>
                    Math.Abs(Tuple.Create(Id).GetHashCode());

        ///<inheritdoc/>
        public string GetParents() =>
            GetType().GetParents();

        ///<inheritdoc/>
        public virtual IReadOnlyList<RemedyParameter> GetState() =>
            new List<RemedyParameter>
            {
                new RemedyParameter(nameof(Id), Id)
            };

        ///<inheritdoc/>
        public virtual string Pack() =>
            $"[{Id}] - [{nameof(IsValid)}: {IsValid}]";

        ///<inheritdoc/>
        public override string ToString() =>
            Pack();

        ///<inheritdoc/>
        public virtual bool Validate()
        {
            return Validate(new RemedyModelValidator()).IsValid;
        }

        ///<inheritdoc/>
        public ValidationResult Validate<TModel>(RemedyValidator<TModel> validator)
            where TModel : RemedyModel, new()
        {
            validator.EnsureNotNull();
            validator.InitRules();

            var validationResult = validator.Validate((TModel)this);

            if (validationResult.Errors is not null)
            {
                ValidationFailures = new BindingList<ValidationFailure>(validationResult.Errors);
            }

            IsValid = validationResult.IsValid;

            return validationResult;
        }

        ///<inheritdoc/>
        protected virtual RemedyModel PerformDeepClone() => new();
    }

    /// <summary> Represents a class for validating <see cref="RemedyModel"/>. </summary>
    public class RemedyModelValidator : RemedyValidator<RemedyModel>
    {
        ///<inheritdoc/>
        public override void RegisterRules()
        {
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .Must(id => id != Guid.Empty)
                .WithMessage($"{nameof(RemedyModel.Id)} should not be empty.")
                .WithSeverity(Severity.Error);

            RuleFor(x => x.GetState())
                .Cascade(CascadeMode.Stop)
                .Must(x => x is not null && x.Any())
                .WithMessage($"{nameof(RemedyModel.GetState)} should have at least 1 value.")
                .WithSeverity(Severity.Error);
        }
    }
}