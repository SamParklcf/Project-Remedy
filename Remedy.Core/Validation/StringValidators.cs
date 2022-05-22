namespace Remedy.Core.Validation
{
    using FluentValidation;

    /// <summary> Provides functionalities to validating string values. </summary>
    public static class StringValidators
    {
        /// <summary> String value should not be null or empty. </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="ruleBuilder"> </param>
        /// <returns> </returns>
        public static IRuleBuilderOptions<T, string> MustNotBeNullOrEmpty<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(x => !string.IsNullOrEmpty(x))
                              .WithMessage("{PropertyName} must not be null or empty")
                              .WithSeverity(Severity.Error);
        }

        /// <summary> String value should not be null or whitespace. </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="ruleBuilder"> </param>
        /// <returns> </returns>
        public static IRuleBuilderOptions<T, string> MustNotBeNullOrWhitespace<T>(
            this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(x => !string.IsNullOrWhiteSpace(x))
                              .WithMessage("{PropertyName} must not be null or whitespace")
                              .WithSeverity(Severity.Error);
        }
    }
}