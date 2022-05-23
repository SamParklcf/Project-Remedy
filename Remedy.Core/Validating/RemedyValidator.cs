namespace Remedy.Core.Validating
{
    using FluentValidation;

    /// <summary> Represents a base class for validating models and objects. </summary>
    /// <typeparam name="T"> </typeparam>
    public abstract class RemedyValidator<T> : AbstractValidator<T>
    {
        private const string _toStringMustNotNullOrEmptyRuleMessage =
             $"'{nameof(ToString)}' method should not return null or empty value, as it responsible for a brief description of an object.";

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
            ToString_Must_Not_Return_Empty();
            ToString_Must_Not_Return_Null();
            GetHashCode_Must_Not_Return_0();

            RegisterRules();
        }

        /// <summary> Add rules to validator. </summary>
        public abstract void RegisterRules();

        /// <summary> Facilitates setting rules for members. </summary>
        /// <param name="mode"> <see cref="CascadeMode"/> for the initiation rule. </param>
        /// <param name="memberExpression"> Expression for fetching the member to ruling on. </param>
        /// <param name="ruleChainAction"> Specifies the continues chain of rules for the member. </param>
        /// <typeparam name="TMemberType"> Type of member. </typeparam>
        /// <exception cref="InvalidOperationException">
        /// Thrown if <paramref name="memberExpression"/> or <paramref name="ruleChainAction"/> be null.
        /// </exception>
        protected void RuleFor<TMemberType>(Expression<Func<T, TMemberType>> memberExpression, Func<IRuleBuilderInitial<T, TMemberType>,
                                                IRuleBuilderOptions<T, TMemberType>> ruleChainAction, CascadeMode mode)
        {
            if (memberExpression is null)
            {
                throw new InvalidOperationException($"{nameof(memberExpression)} is not provided.");
            }

            if (ruleChainAction is null)
            {
                throw new InvalidOperationException($"{nameof(ruleChainAction)} is not provided.");
            }

            ruleChainAction(RuleFor(memberExpression)
                               .Cascade(mode));
        }

        /// <summary> Sets rule: <see cref="object.GetHashCode"/> must not returns empty value. </summary>
        private void GetHashCode_Must_Not_Return_0()
        {
            RuleFor(x => x.GetHashCode(),
                    x => x.NotEmpty()
                          .WithSeverity(Severity.Error),
                    CascadeMode.Stop);
        }

        /// <summary> Sets rule: <see cref="object.ToString"/> must not returns null value. </summary>
        private void ToString_Must_Not_Return_Empty()
        {
            RuleFor(x => x.ToString(),
                    x => x.NotEmpty()
                          .WithSeverity(Severity.Warning)
                          .WithMessage(_toStringMustNotNullOrEmptyRuleMessage),
                    CascadeMode.Continue);
        }

        /// <summary> Sets rule: <see cref="object.ToString"/> must not returns empty value. </summary>
        private void ToString_Must_Not_Return_Null()
        {
            RuleFor(x => x.ToString(),
                    x => x.NotNull()
                          .WithMessage(_toStringMustNotNullOrEmptyRuleMessage),
                    CascadeMode.Stop);
        }
    }
}