namespace Remedy.Test.Core.Extensions
{
    using Remedy.Core.Extensions;

    public class EqualityCheckersTests
    {
        [Fact]
        public void DifferentStudents_Should_NoBeEquals()
        {
            var student1 = new A
            {
                Prop1 = "Sam Park"
            };

            var student2 = new A
            {
                Prop1 = "Sam Park"
            };

            student2.Equals(student1).Should()
                .BeFalse();

            student2.Equals<A>(student1).Should()
                .BeFalse();
        }

        [Fact]
        public void DifferentStudentsWithSameFullName_Should_BeEquals()
        {
            var student1 = new A
            {
                Prop1 = "Sam Park"
            };

            var student2 = new A
            {
                Prop1 = "Sam Park"
            };

            student2.Equals<A>(x =>
                x.Prop1 == student1.Prop1).Should()
                .BeTrue();
        }

        [Fact]
        public void OfTypeForSameObjectsType_Should_ReturnTrue()
        {
            var a = new A();

            a.OfType<A>().Should()
                .BeTrue();
        }

        [Fact]
        public void SameStudents_Should_BeEquals()
        {
            var student1 = new A
            {
                Prop1 = "Sam Park"
            };

            var student2 = student1;

            student2.Equals(student1).Should()
                .BeTrue();

            student2.Equals<A>(student1).Should()
                .BeTrue();

            student2.Equals<A>(x =>
                x.Prop1 == student1.Prop1).Should()
                .BeTrue();
        }

        public class A
        {
            public string Prop1 { get; set; }
        }
    }
}