namespace Remedy.Test.Core
{
    using Remedy.Core;
    using Remedy.Test.Base;

    public class RemedyParameterTests
    {
        [Fact]
        public void CreateTwoDifferentObjects_Should_NotBeEqual()
        {
            const string paramName = "Full FirstName";

            var param1 = new RemedyParameter(paramName, "Sam");
            var param2 = new RemedyParameter(paramName, "Sam Park");

            param1.Equals(param2).Should()
                .BeFalse();

            var genericParam1 = new RemedyParameter<string>(paramName, "Sam");
            var genericParam2 = new RemedyParameter<string>(paramName, "Sam Park");

            genericParam1.Equals(genericParam2).Should()
                .BeFalse();
        }

        [Fact]
        public void CreateTwoDifferentObjectsTypes_Should_NotBeEqual()
        {
            const string paramName = "Full FirstName";

            var param1 = new RemedyParameter(paramName, "Sam");
            var param2 = "Sam";

            param1.Equals(param2).Should()
                .BeFalse();

            var genericParam1 = new RemedyParameter<string>(paramName, "Sam");
            var genericParam2 = "Sam";

            genericParam1.Equals(genericParam2).Should()
                .BeFalse();
        }

        [Fact]
        public void CreateTwoSameObjects_Should_BeEqual()
        {
            const string paramName = "Full FirstName";
            const string paramValue = "Sam Park";

            var param1 = new RemedyParameter(paramName, paramValue);
            var param2 = new RemedyParameter(paramName, paramValue);

            param1.Equals(param2).Should()
                .BeTrue();

            var genericParam1 = new RemedyParameter<string>(paramName, paramValue);
            var genericParam2 = new RemedyParameter<string>(paramName, paramValue);

            genericParam1.Equals(genericParam2).Should()
                .BeTrue();
        }

        [Fact]
        public void EmptyName_ShouldThrow_ArgumentNullException()
        {
            new Action(() => new RemedyParameter("", null))
                .ExpectArgumentNullException();

            new Action(() => new RemedyParameter<string>("", null))
               .ExpectArgumentNullException();
        }

        [Fact]
        public void GetHashCode_Should_GeneratesByNameAndValue()
        {
            const string paramName = "Full FirstName";
            const string paramValue = "Sam Park";

            var param = new RemedyParameter(paramName, paramValue);

            param.GetHashCode().Should()
                .BeGreaterThan(0)
                .And
                .Be(Math.Abs(Tuple.Create(paramName, paramValue).GetHashCode()));

            var genericParam = new RemedyParameter<string>(paramName, paramValue);

            genericParam.GetHashCode().Should()
                .BeGreaterThan(0)
                .And
                .Be(Math.Abs(Tuple.Create(paramName, paramValue).GetHashCode()));
        }

        [Fact]
        public void Initialization_Should_HaveSameValuesAndTypes()
        {
            const string paramName = "Full FirstName";
            const string paramValue = "Sam Park";

            var param = new RemedyParameter(paramName, paramValue);

            param.Name.Should()
                .NotBeNullOrWhiteSpace()
                .And
                .BeSameAs(paramName);
            param.Value.Should()
                .NotBeNull()
                .And
                .BeOfType<string>()
                .And
                .Be(paramValue);

            var genericParam = new RemedyParameter<string>(paramName, paramValue);

            genericParam.Name.Should()
                .NotBeNullOrWhiteSpace()
                .And
                .BeSameAs(paramName);
            genericParam.Value.Should()
               .NotBeNull()
               .And
               .BeOfType<string>()
               .And
               .Be(paramValue);
        }

        [Fact]
        public void NullValue_ShouldThrow_ArgumentNullException()
        {
            new Action(() => new RemedyParameter("Param name", null))
                .ExpectArgumentNullException();

            new Action(() => new RemedyParameter<string>("Param name", null))
               .ExpectArgumentNullException();
        }

        [Fact]
        public void ToString_Should_NotBeEmpty()
        {
            const string paramName = "Full FirstName";
            const string paramValue = "Sam Park";

            var param = new RemedyParameter(paramName, paramValue);

            param.ToString().Should()
                .NotBeNullOrWhiteSpace();
        }
    }
}