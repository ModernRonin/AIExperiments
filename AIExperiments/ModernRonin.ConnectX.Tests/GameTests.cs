using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace ModernRonin.ConnectX.Tests
{
    [TestFixture]
    public class GameTests
    {
        [Test]
        public void Constructor_Initializes_Board()
        {
            new Game(GameConfiguration.Default).Board.Should().NotBeNull();
        }
        [Test]
        public void Constructor_Sets_PlayerToMoveId_To_Zero()
        {
            new Game(GameConfiguration.Default).PlayerToMove.Should().Be(0);
        }
        [Test]
        public void Constructor_Sets_RemainingStones_From_Configuration_For_Both_Players()
        {
            var underTest = new Game(GameConfiguration.Default);
            underTest.RemainingStones(0).Should().HaveCount(21).And.Subject
                     .All(s => s.Owner == 0 && s.Kind == StoneKind.Regular).Should().BeTrue();
            underTest.RemainingStones(1).Should().HaveCount(21).And.Subject
                     .All(s => s.Owner == 1 && s.Kind == StoneKind.Regular).Should().BeTrue();
        }
        [Test]
        public void Execute_Removes_Stone_From_RemainingStones()
        {
            var underTest = new Game(GameConfiguration.Default);
            underTest.Execute(new Move());

            underTest.RemainingStones(0).Should().HaveCount(20);
        }
        [Test]
        public void Execute_Sets_Board()
        {
            var underTest = new Game(GameConfiguration.Default);
            underTest.Execute(new Move());

            underTest.Board[0, 0].Should().BeEquivalentTo(new Stone());

            underTest.Execute(new Move());
            underTest.Board[0, 1].Should().BeEquivalentTo(new Stone(StoneKind.Regular, 1));

            underTest.Execute(new Move());
            underTest.Board[0, 2].Should().BeEquivalentTo(new Stone());
        }
        [Test]
        public void Execute_Toggles_PlayerToMove()
        {
            var underTest = new Game(GameConfiguration.Default);
            underTest.Execute(new Move());

            underTest.PlayerToMove.Should().Be(1);

            underTest.Execute(new Move());

            underTest.PlayerToMove.Should().Be(0);
        }
    }
}