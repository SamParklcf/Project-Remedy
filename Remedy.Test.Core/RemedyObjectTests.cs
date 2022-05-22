namespace Remedy.Test.Core
{
    using Remedy.Core;
    using Remedy.Test.Base;

    public class RemedyObjectTests
    {
        [Fact]
        public async void DisposeTests()
        {
            RemedyCore.Init();
            var obj = new RemedyObject();

            obj.Disposed.Should()
                .BeFalse();
            obj.IsObjectAlive().Should()
                .BeTrue();

            obj.Dispose();
            obj.Disposed.Should()
                .BeTrue();
            obj.IsObjectAlive().Should()
                .BeFalse();

            obj = new RemedyObject();
            obj.Disposed.Should()
                .BeFalse();

            await obj.DisposeAsync();
            obj.Disposed.Should()
                .BeTrue();
            obj.IsObjectAlive().Should()
                .BeFalse();

            obj.GetBirthTime().Should()
                .NotBe(DateTime.MinValue);
            obj.GetDeathTime().Should()
                .NotBe(DateTime.MinValue);
            obj.GetALiveTime().Should()
                .BeGreaterThan(TimeSpan.Zero);
        }

        [Fact]
        public void InitWithNullLifeTracker_Should_ThrowArgumentNullException()
        {
            var action = new Action(() => new RemedyObject(null));

            action.ExpectArgumentNullException();
        }

        [Fact]
        public void InitWithValidLifeTracker_Should_HaveLifeTracker()
        {
            var obj = new RemedyObject(new RemedyLifeTracker());

            obj.GetALiveTime().Should()
                .BeGreaterThan(TimeSpan.Zero);
            obj.IsObjectAlive().Should()
                .BeTrue();
        }

        [Fact]
        public void RemedyObjectInitWithDefaultConstructor_Should_HaveValidLifeTracker()
        {
            RemedyCore.Init();
            var obj = new RemedyObject();
            obj.GetALiveTime().Should()
                .BeGreaterThan(TimeSpan.Zero);
        }

        [Fact]
        public void ValidationTests()
        {
            RemedyCore.Init();
            var obj = new RemedyObject();

            obj.Validate();
            obj.IsValid.Should()
                .BeTrue();
        }
    }
}