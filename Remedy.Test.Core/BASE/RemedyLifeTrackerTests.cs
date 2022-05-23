namespace Remedy.Test.Core
{
    using Remedy.Core;
    using Remedy.Core.Extensions.Reflection;

    public class RemedyLifeTrackerTests
    {
        [Fact]
        public void EnumerateParentsResult_Should_HaveCount()
        {
            var obj = new RemedyLifeTracker();

            obj.EnumerateParents().Should()
                .HaveCount(2);
        }

        [Fact]
        public void GetHashCodeResult_Should_BeGreaterThanZero()
        {
            var lifeTracker = new RemedyLifeTracker();

            lifeTracker.GetHashCode().Should()
                .BeGreaterThan(0);
        }

        [Fact]
        public void GetParentsResult_Should_ContainsRemedyNotifierGetParentsResult()
        {
            var obj = new RemedyLifeTracker();

            obj.GetParents().Should()
                .Contain(new RemedyNotifier().GetParents());
        }

        [Fact]
        public void GetState_Should_HaveValue()
        {
            var lifeTracker = new RemedyLifeTracker();
            lifeTracker.RegisterDeath();

            var state = lifeTracker.GetState();
            state.Should()
                .HaveCount(4)
                .And
                .OnlyHaveUniqueItems();
        }

        [Fact]
        public void Intialization_Should_HaveBirthTimeAndDeathTime()
        {
            var lifeTracker = new RemedyLifeTracker();

            lifeTracker.GetBirthTime().Should()
                .BeBefore(DateTime.Now);
            lifeTracker.GetDeathTime().Should()
                .Be(DateTime.MinValue);

            lifeTracker.IsObjectAlive().Should()
                .BeTrue();

            lifeTracker.RegisterDeath();
            lifeTracker.IsObjectAlive().Should()
                .BeFalse();
            lifeTracker.GetDeathTime().Should()
                .NotBe(DateTime.MinValue);

            (lifeTracker.GetDeathTime() - lifeTracker.GetBirthTime()).Should()
                .Be(lifeTracker.GetALiveTime());
        }

        [Fact]
        public void ToStringResult_Should_ContainsValue()
        {
            var obj = new RemedyLifeTracker();

            obj.ToString().Should()
                .Contain("IsAlive")
                .And
                .Contain("Duration");
        }

        [Fact]
        public void TwoObjects_Should_NeverBeEquals()
        {
            var lifeTracker1 = new RemedyLifeTracker();
            var lifeTracker2 = new RemedyLifeTracker();

            lifeTracker1.Equals(lifeTracker2).Should()
                .BeFalse();
        }
    }
}