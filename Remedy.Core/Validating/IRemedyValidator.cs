namespace Remedy.Core.Validating
{
    using System.ComponentModel;

    using FluentValidation.Results;

    /// <summary> Provides functionalities to validating models and objects. </summary>
    /// <typeparam name="T"> </typeparam>
    public interface IRemedyValidator<T>
    {
        /// <summary> Determines if model is valid or not. </summary>
        bool IsValid { get; }

        /// <summary>
        /// Gets or sets <see cref="RemedyModel"/><see cref="ValidationFailure"/> s.
        /// </summary>
        BindingList<ValidationFailure> ValidationFailures { get; }

        /// <summary> Validates model using provides <paramref name="validator"/>. </summary>
        /// <typeparam name="TModel"> Type of model to be validated. </typeparam>
        /// <param name="validator"> Validator to check <typeparamref name="TModel"/> validation. </param>
        /// <returns> <see cref="ValidationResult"/> represents the result of validation. </returns>
        ValidationResult Validate<TModel>(RemedyValidator<TModel> validator)
                            where TModel : RemedyModel, new();

        /// <summary> Checks if model is valid or not. </summary>
        /// <returns> True if model is valid, otherwise false. </returns>
        bool Validate();
    }
}