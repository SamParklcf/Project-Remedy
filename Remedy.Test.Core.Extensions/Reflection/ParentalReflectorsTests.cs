namespace Remedy.Test.Core.Extensions.Reflection
{
    using Remedy.Core.Extensions.Reflection;

    public class ParentalReflectorsTests
    {
        [Fact]
        public void EnumerateParentsResultForClassA_Should_NotBeEmpty()
        {
            var a = new A();

            a.GetType().EnumerateParents().Should()
                .NotBeEmpty()
                .And
                .HaveCount(1);

            a.EnumerateParents().Should()
                .NotBeEmpty()
                .And
                .HaveCount(1);
        }

        [Fact]
        public void GetParentsResultForClassBAndD_Should_BeTheSame()
        {
            var b = new B();
            var d = new D();

            b.GetType().GetParents().Should()
                .Be(
                    d.GetType().GetParents());

            b.GetParents().Should()
               .Be(
                   d.GetParents());
        }

        [Fact]
        public void GetParentsResultForClassC_Should_ContainsClassBGetParents()
        {
            var b = new B();
            var c = new C();

            c.GetType().GetParents().Should()
                .Contain(b.GetType().GetParents())
                .And
                .Contain(ParentalReflectors.EndValue);

            c.GetParents().Should()
                .Contain(b.GetParents())
                .And
                .Contain(ParentalReflectors.EndValue);
        }

        [Fact]
        public void GetParentsWithNullValue_Should_ReturnEndValue()
        {
            A obj = null;

            obj?.GetType().GetParents().Should()
                .Be(ParentalReflectors.EndValue);

            obj?.GetParents().Should()
                .Be(ParentalReflectors.EndValue);
        }
    }
}