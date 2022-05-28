namespace Remedy.Test.Extensions.Base.TypeCheckersTests
{
    using Remedy.Extensions.Base.TypeCheckers;
    using Remedy.Test.Base;

    public class TypeCheckersTests
    {
        [Fact]
        public void EnsureNotNull_ForNullObject_ShouldThrownArgumentNullException()
        {
            A classA = null;
            var action = () => classA.EnsureNotNull("Null provided.");

            action.ExpectArgumentNullException();
        }

        [Fact]
        public void EnsureNotNull_ForNullObject_ShouldThrownCustomArgumentException()
        {
            A classA = null;
            var action = () => classA.EnsureNotNull(new ArgumentException());

            action.ExpectArgumentException();
        }

        [Fact]
        public void GetValueIfNotNull_Should_ReturnValueIfNotBeNull()
        {
            var a = new A();
            a.Prop1 = "Prop1";

            var b = a.GetValueIfNotNull(new ArgumentNullException());

            b.Should()
                .NotBeNull();

            b.Prop1.Should()
                .Be(a.Prop1);
        }

        public class A
        {
            public string Prop1 { get; set; }
        }
    }
}