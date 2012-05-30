using BDDfy.Core;
using NUnit.Framework;

namespace BDDfy.Tests
{
    [Story(
        AsA = "As a programmer",
        IWant = "I want to first create an empty story",
        SoThat = "So that I can do test first development")]
    [TestFixture]
    public class WhenAnEmptyStoryIsBddified
    {
        [Test]
        public void NoExeptionIsThrown()
        {
            this.BDDfy();
        }
    }
}