using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace ModernRonin.ConnectX.Tests
{
    [TestFixture]
    public class GameConfigurationTests
    {
        [Test]
        public void Default_Has_Width_7()
        {
            GameConfiguration.Default.BoardWidth.Should().Be(7);
        }
        [Test]
        public void Default_Has_Height_6()
        {
            GameConfiguration.Default.BoardHeight.Should().Be(6);
        }
        [Test]
        public void Default_Has_21_Regular_InitialStonePerPlayer()
        {
            GameConfiguration.Default.InitialStonesPerPlayer.Count().Should().Be(21);
            GameConfiguration.Default.InitialStonesPerPlayer.All(k => k == StoneKind.Regular).Should().BeTrue();
        }
    }
}

