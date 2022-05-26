namespace Remedy.Extensions.Base.Reflection
{
    using System.Linq.Expressions;

    /// <summary> Provides extensions to work with property of type information. </summary>
    public static class PropertyMemberReflectors
    {
        /// <summary> Gets property name from expression. </summary>
        /// <param name="propertyExpression"> The property expression. </param>
        /// <typeparam name="T"> </typeparam>
        /// <returns> The <see cref="string"/>. </returns>
        /// <exception cref="ArgumentNullException">
        /// Throws an exception if expression is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Expression should be a member access lambda expression
        /// </exception>
        public static string GetPropertyName<T>(this Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression is null)
            {
                throw new ArgumentNullException(nameof(propertyExpression));
            }

            if (propertyExpression.Body.NodeType != ExpressionType.MemberAccess)
            {
                throw new ArgumentException("Should be a member access lambda expression", nameof(propertyExpression));
            }

            var memberExpression = (MemberExpression)propertyExpression.Body;
            return memberExpression.Member.Name;
        }
    }
}