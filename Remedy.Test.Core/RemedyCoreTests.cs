namespace Remedy.Test.Core
{
    using Remedy.Core;

    public class RemedyCoreTests
    {
        [Fact]
        public void Init_Should_InitializeServiceProvider()
        {
            RemedyCore.Init();

            RemedyCore.CoreInitialized.Should()
                .BeTrue();
        }

        [Fact]
        public void InitWithCustomLifeTracker_Should_HaveCustomLifeTracker()
        {
            RemedyCore.Init(new CustomLifeTracker());

            RemedyCore.CoreInitialized.Should()
                .BeTrue();

            RemedyCore.CreateNewLifeTracker().Should()
                .BeOfType<CustomLifeTracker>();
        }

        public class CustomLifeTracker : RemedyLifeTracker, IRemedyLifeTracker
        {
            public string Name { get; set; }
        }
    }
}