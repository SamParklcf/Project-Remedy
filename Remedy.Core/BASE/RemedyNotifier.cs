namespace Remedy.Core
{
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;

    using Extensions;

    using Remedy.Core.Extensions.Reflection;

    /// <summary>
    /// Represents a base class to providing <see cref="INotifyPropertyChanged"/> mechanism for an object.
    /// </summary>
    public class RemedyNotifier : INotifyPropertyChanged
    {
        /// <summary> Occurs when property is changed. </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Changes the property if the value is different and raises the PropertyChanged event.
        /// </summary>
        /// <typeparam name="T"> The 1st type parameter. </typeparam>
        /// <param name="backingStore"> Reference to current value. </param>
        /// <param name="value"> New value to be set. </param>
        /// <param name="propertyExpression">
        /// The lambda expression of the property to raise the PropertyChanged event for.
        /// </param>
        /// <returns> <c> true </c> if new value, <c> false </c> otherwise. </returns>
        protected bool SetProperty<T>(ref T backingStore, T value, Expression<Func<T>> propertyExpression) =>
            SetProperty(ref backingStore, value, propertyExpression.GetPropertyName());

        /// <summary>
        /// Changes the property if the value is different and raises the PropertyChanged event.
        /// </summary>
        /// <typeparam name="T"> The 1st type parameter. </typeparam>
        /// <param name="backingStore"> Reference to current value. </param>
        /// <param name="value"> New value to be set. </param>
        /// <param name="propertyName">
        /// The name of the property to raise the PropertyChanged event for.
        /// </param>
        /// <returns> <c> true </c> if new value, <c> false </c> otherwise. </returns>
        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "")
        {
            if (backingStore.Equals<T>(value))
            {
                return false;
            }

            backingStore = value;
            TellPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Changes the property if the value is different and raises the PropertyChanged event.
        /// </summary>
        /// <returns> <c> true </c>, if property was set, <c> false </c> otherwise. </returns>
        /// <param name="backingStore"> Backing store. </param>
        /// <param name="value"> Value. </param>
        /// <param name="propertyName"> Property name. </param>
        /// <param name="onChanged"> On changed. </param>
        /// <typeparam name="T"> The 1st type parameter. </typeparam>
		protected virtual bool SetProperty<T>(ref T backingStore, T value,
            Action onChanged,
            [CallerMemberName] string propertyName = "") =>
            SetProperty<T>(ref backingStore, value, onChanged, null, propertyName);

        /// <summary>
        /// Changes the property if the value is different and raises the PropertyChanged event.
        /// </summary>
        /// <returns> <c> true </c>, if property was set, <c> false </c> otherwise. </returns>
        /// <param name="backingStore"> Backing store. </param>
        /// <param name="value"> Value. </param>
        /// <param name="validateValue"> Validates value. </param>
        /// <param name="propertyName"> Property name. </param>
        /// <param name="onChanged"> On changed. </param>
        /// <typeparam name="T"> The 1st type parameter. </typeparam>
		protected virtual bool SetProperty<T>(ref T backingStore, T value,
            Action onChanged, Func<T, T, bool> validateValue,
            [CallerMemberName] string propertyName = "")
        {
            if (backingStore.Equals<T>(value))
                return false;

            if (validateValue != null && !validateValue(backingStore, value))
                return false;

            backingStore = value;
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
            onChanged?.Invoke();
            return true;
        }

        /// <summary> Raises the PropertyChanged event. </summary>
        /// <param name="propertyName">
        /// The name of the property to raise the PropertyChanged event for.
        /// </param>
		protected void TellPropertyChanged([CallerMemberName] string propertyName = null) =>
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));

        /// <summary> Raises the PropertyChanged event. </summary>
        /// <typeparam name="T"> The 1st type parameter. </typeparam>
        /// <param name="propertyExpression">
        /// The lambda expression of the property to raise the PropertyChanged event for.
        /// </param>
		protected void TellPropertyChanged<T>(Expression<Func<T>> propertyExpression) =>
            OnPropertyChanged(new PropertyChangedEventArgs(propertyExpression.GetPropertyName()));

        /// <summary> The property changed event invoker. </summary>
        /// <param name="e"> The event arguments. </param>
        private void OnPropertyChanged(PropertyChangedEventArgs e) =>
             PropertyChanged?.Invoke(this, e);
    }
}