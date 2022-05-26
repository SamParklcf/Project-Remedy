namespace Remedy.Core.Validating
{
    using FluentValidation;

    /// <summary> Represents a base class for validating models and objects. </summary>
    /// <typeparam name="T"> </typeparam>
    public abstract class RemedyValidator<T> : AbstractValidator<T>
    {
        /// <summary> Intiializes a new instance of <see cref="RemedyValidator{T}"/> class. </summary>
        protected RemedyValidator()
        {
        }

        /// <summary>
        /// Initializes validating rules. this method calls <see cref="RegisterRules"/> to
        /// initializes registered rules.
        /// </summary>
        public void InitRules()
        {
            ToString_Must_Not_Return_NullOrEmpty();
            GetHashCode_Must_Not_Return_0();

            RegisterRules();
        }

        /// <summary> Add rules to validator. </summary>
        public abstract void RegisterRules();

        /// <summary> Sets rule: <see cref="object.GetHashCode"/> must not returns empty value. </summary>
        private void GetHashCode_Must_Not_Return_0()
        {
            RuleFor(x => x.GetHashCode())
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0)
                .WithMessage($"{nameof(GetHashCode)} should have value greater than 0.")
                .WithSeverity(Severity.Error);
        }

        /// <summary> Sets rule: <see cref="object.ToString"/> must not returns null value. </summary>
        private void ToString_Must_Not_Return_NullOrEmpty()
        {
            RuleFor(x => x.ToString())
               .Cascade(CascadeMode.Stop)
               .Must(x => !string.IsNullOrWhiteSpace(x))
               .WithMessage($"{nameof(ToString)} should not be null or empty as it is a brief object descriptor.")
               .WithSeverity(Severity.Error);
        }
    }
}