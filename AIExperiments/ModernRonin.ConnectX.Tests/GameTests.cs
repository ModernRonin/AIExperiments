using FluentAssertions;
using NUnit.Framework;

namespace ModernRonin.ConnectX.Tests
{
    [TestFixture]
    public class GameTests
    {
        [Test]
        public void Constructor_Sets_PlayerToMoveId_To_Zero()
        {
            new Game(GameConfiguration.Default).PlayerToMoveId.Should().Be(0);
        }
        [Test]
        public void Constructor_Sets_RemainingStones_From_Configuration_For_Both_Players
            ()
        {
            var underTest = new Game(GameConfiguration.Default);
            underTest.RemainingStones(0).Should().AllBeEquivalentTo(underTest.RemainingStones(1));
        }
    }
}

