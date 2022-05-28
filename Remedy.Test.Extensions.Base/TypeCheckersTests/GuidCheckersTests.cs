namespace Remedy.Test.Extensions.Base.TypeCheckersTests
{
    using Remedy.Extensions.Base.TypeCheckers;
    using Remedy.Test.Base;

    public class GuidCheckersTests
    {
        [Fact]
        public void EnsureNotEmpty_Should_ThrowArgumentExceptionOnEmptyInput()
        {
            var id = Guid.Empty;
            var action = () => id.EnsureNotEmpty();

            action.ExpectArgumentException();
        }

        [Fact]
        public void GetValueIfNotEmpty_ShouldReturnValue_IfInputNotEmpty()
        {
            var id = Guid.NewGuid();

            var anotherId1 = id.GetValueIfNotEmpty();
            var anotherId2 = id.GetValueIfNotEmpty(new ArgumentException());

            anotherId1.Should()
                .Be(id);
            anotherId2.Should()
               .Be(id);
        }
    }
}