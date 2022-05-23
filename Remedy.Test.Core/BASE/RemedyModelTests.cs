namespace Remedy.Test.Core
{
    using System;

    using Remedy.Core;
    using Remedy.Core.Extensions;
    using Remedy.Core.Extensions.Reflection;
    using Remedy.Test.Base;

    public class RemedyModelTests
    {
        [Fact]
        public void ChangeRefPropertyInParentObject_Should_AffectOnClonedObject()
        {
            var model1 = new Student()
            {
                Name = "Sam Park",
                Address = new Address
                {
                    City = "London"
                }
            };
            var model2 = model1.Clone() as Student;

            model1.Name = "Sam Park - Changed";
            model1.Name.Should()
                .NotBe(model2.Name);

            model1.Address.City = "LA";
            model1.Address.City.Should()
                .Be(model2.Address.City);
        }

        [Fact]
        public void CloneObject_Should_BeEqual()
        {
            var model1 = new Student()
            {
                Name = "Sam Park",
                Address = new Address
                {
                    City = "London"
                }
            };
            var model2 = model1.Clone() as Student;

            model1.Equals(model2).Should()
                .BeTrue();
        }

        [Fact]
        public void DeepCloneObject_Should_BeEqual()
        {
            var model1 = new Student()
            {
                Name = "Sam Park",
                Address = new Address
                {
                    City = "London"
                }
            };
            var model2 = model1.DeepClone() as Student;

            model1.Equals(model2).Should()
                .BeTrue();
        }

        [Fact]
        public void DifferentObjects_Should_NotBeEqual()
        {
            var model1 = new RemedyModel();
            var model2 = new RemedyModel();

            model1.Equals(model2).Should()
                .BeFalse();
        }

        [Fact]
        public void EnumerateParentsResult_Should_HaveCount()
        {
            var obj = new RemedyModel();

            obj.EnumerateParents().Should()
                .HaveCount(2);
        }

        [Fact]
        public void GenericClone_Should_CloneRightType()
        {
            var model1 = new Student();
            var model2 = model1.Clone<Student>();
            var model3 = model1.Clone<RemedyModel>();

            var model4 = model1.DeepClone<Student>();
            var model5 = model1.DeepClone<RemedyModel>();

            var action = new Action(() => model1.Clone<Address>());
            action.ExpectInvalidCastException();
        }

        [Fact]
        public void GetHashCodeResult_Should_BeEqualWithManualGetHashCode()
        {
            var id = Guid.NewGuid();

            var student = new Student(id);

            student.GetHashCode().Should()
                .BeGreaterThan(0)
                .And
                .Be(Math.Abs(Tuple.Create(id).GetHashCode()));
        }

        [Fact]
        public void GetParentsResult_Should_ContainsRemedyNotifierGetParentsResult()
        {
            var obj = new RemedyModel();

            obj.GetParents().Should()
                .Contain(new RemedyNotifier().GetParents());
        }

        [Fact]
        public void GetStateResult_Should_HaveCount()
        {
            var obj = new RemedyModel();

            obj.GetState().Should()
                .HaveCount(1);
        }

        [Fact]
        public void InitWithDefaultConstrcutor_Should_HaveId()
        {
            var obj = new RemedyModel();

            obj.Id.Should()
                .NotBeEmpty();
        }

        [Fact]
        public void InitWithIdConstrcutorByCorrectProvider_Should_HaveId()
        {
            var obj = new Student(Guid.NewGuid());

            obj.Id.Should()
                .NotBeEmpty();
        }

        [Fact]
        public void InitWithIdConstrcutorByEmptyProvider_Should_ThrowArgumentException()
        {
            var action = new Action(() => new Student(Guid.Empty));

            action.ExpectArgumentException();
        }

        [Fact]
        public void RemedyObject_Should_BeAssignableToRemedyNotifierAndIRemedyState()
        {
            var obj = new RemedyModel();

            obj.Should()
                .BeAssignableTo<RemedyNotifier>()
                .And
                .BeAssignableTo<IRemedyState>();
        }

        [Fact]
        public void ToStringResult_Should_ContainIdValue()
        {
            var obj = new RemedyModel();

            obj.ToString().Should()
                .Contain(obj.Id.ToString());
        }

        [Fact]
        public void ValidationTests()
        {
            var model = new RemedyModel();
            model.Validate();

            model.IsValid.Should()
                .BeTrue();
            model.ValidationFailures.Should()
                .HaveCount(0);

            var action = new Action(() => model.Validate<RemedyModel>(null));
            action.ExpectArgumentNullException();
        }

        public class Address : RemedyModel
        {
            private string _city;

            public string City
            {
                get => _city;
                set => SetProperty(ref _city, value);
            }

            public override bool Equals(object obj)
            {
                return obj.Equals<Address>(x =>
                    x.City == City);
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }

        public class Student : RemedyModel
        {
            private Address _address;
            private string _name;

            public Student()
            {
            }

            public Student(Guid id) : base(id)
            {
            }

            public Address Address
            {
                get => _address;
                set => SetProperty(ref _address, value);
            }

            public string Name
            {
                get => _name;
                set => SetProperty(ref _name, value);
            }

            public override bool Equals(object obj)
            {
                return obj.Equals<Student>(x =>
                    x.Name == Name &&
                    x.Address.Equals(Address));
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

            protected override RemedyModel PerformDeepClone()
            {
                var baseModel = base.PerformDeepClone();

                return new Student(baseModel.Id)
                {
                    Name = Name,
                    Address = new Address
                    {
                        City = Address?.City
                    }
                };
            }
        }
    }
}