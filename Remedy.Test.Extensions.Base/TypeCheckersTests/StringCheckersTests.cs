namespace Remedy.Test.Extensions.Base.TypeCheckersTests
{
    using Remedy.Extensions.Base.TypeCheckers;
    using Remedy.Test.Base;

    public class StringCheckersTests
    {
        [Fact]
        public void EnsureNotNullOrEmpty_ForNullString_ShouldThrownCustomArgumentNullException()
        {
            string str = null;
            var action = () => str.EnsureNotNullOrEmpty();

            action.ExpectArgumentNullException();
        }

        [Fact]
        public void EnsureNotNullOrWhitespace_ForNullString_ShouldThrownCustomArgumentNullException()
        {
            string str = null;
            var action = () => str.EnsureNotNullOrWhitespace();

            action.ExpectArgumentNullException();
        }

        [Fact]
        public void GetValueIfNotNullOrEmpty_Should_ReturnStringIfNotNullableString()
        {
            string str = "Hello";

            var b = str.GetValueIfNotNullOrEmpty();
            var c = str.GetValueIfNotNullOrEmpty();

            b.Should()
                .NotBeNull();
            c.Should()
               .NotBeNull();

            b.Should()
                .Be(str)
                .And
                .Be(c);
        }

        [Fact]
        public void GetValueIfNotNullOrWhitespace_Should_ReturnStringIfNotNullableString()
        {
            string str = "Hello";

            var b = str.GetValueIfNotNullOrWhitespace();
            var c = str.GetValueIfNotNullOrWhitespace();

            b.Should()
                .NotBeNull();
            c.Should()
               .NotBeNull();

            b.Should()
                .Be(str)
                .And
                .Be(c);
        }
    }
}