namespace Remedy.Test.Extensions.Base.Reflection
{
    using System.Linq.Expressions;

    using Remedy.Extensions.Base.Reflection;
    using Remedy.Test.Base;

    public class PropertyMemberReflectorsTests
    {
        internal string Prop1 { get; set; }

        [Fact]
        public void GetPropertyNameWithoutPropertyExpression_Should_ThrowArgumentNullException()
        {
            var action = new Action(() => GetPropName<string>(null));

            action.ExpectArgumentNullException();
        }

        [Fact]
        public void GetPropertyNameWithPropertyExpression_Should_HaveProp1Name()
        {
            Prop1 = "";

            var propName = GetPropName(() => Prop1);
            propName.Should()
                .Be(nameof(Prop1));

            Prop1.Should()
                .BeNullOrEmpty();
        }

        [Fact]
        public void GetPropertyNameWithWrongPropertyExpression_Should_ThrowArgumentException()
        {
            var action = new Action(() => GetPropName(() => ""));

            action.ExpectArgumentException();
        }

        private string GetPropName<T>(Expression<Func<T>> propertyExpression) => propertyExpression.GetPropertyName();
    }
}