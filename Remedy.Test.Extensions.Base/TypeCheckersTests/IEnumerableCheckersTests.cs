namespace Remedy.Test.Extensions.Base.TypeCheckersTests
{
    using System.Collections;

    using Remedy.Extensions.Base.TypeCheckers;
    using Remedy.Test.Base;

    public class IEnumerableCheckersTests
    {
        [Fact]
        public void EnsureHaveCount_For3Items_ShouldNotThrownArgumentException_ForInputWith3Item()
        {
            var items = new List<int> { 11, 21, 33 };

            var action = () => items.EnsureHaveCount(3, "Should have 3 items");

            action.Should()
                .NotThrow();
        }

        [Fact]
        public void EnsureHaveCount_For3Items_ShouldThrownArgumentException_ForInputWith1Item()
        {
            var items = new List<int> { 21 };

            var action = () => items.EnsureHaveCount(3, "Should have 3 items");

            action.ExpectArgumentException();
        }

        [Fact]
        public void EnsureNotNull_ForNullIEnumerable_ShouldThrownArgumentNullException()
        {
            IEnumerable values = null;

            var action = () => values.EnsureNotNull();

            action.ExpectArgumentNullException();
        }
    }
}