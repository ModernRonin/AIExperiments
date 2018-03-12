using FluentAssertions;
using NUnit.Framework;

namespace ModernRonin.ConnectX.Tests
{
    [TestFixture]
    public class BoardTests
    {
        [Test]
        public void Copy_Constructor_Copies_Height()
        {
            var original = new Board(new GameConfiguration {BoardWidth = 3, BoardHeight = 2});
            var copy = new Board(original);

            copy.Height.Should().Be(2);
        }
        [Test]
        public void Copy_Constructor_Copies_Stones()
        {
            var original = new Board(new GameConfiguration {BoardWidth = 3, BoardHeight = 2})
            {
                [0, 0] = new Stone(StoneKind.Regular, 0),
                [1, 0] = new Stone(StoneKind.Regular, 1),
                [2, 1] = new Stone(StoneKind.Regular, 0)
            };
            var copy = new Board(original);

            copy[0, 0].Owner.Should().Be(0);
            copy[1, 0].Owner.Should().Be(1);
            copy[2, 1].Owner.Should().Be(0);
            copy[0, 1].Owner.Should().Be(-1);
            copy[1, 1].Owner.Should().Be(-1);
            copy[2, 0].Owner.Should().Be(-1);
        }
        [Test]
        public void Copy_Constructor_Copies_Width()
        {
            var original = new Board(new GameConfiguration {BoardWidth = 3, BoardHeight = 2});
            var copy = new Board(original);

            copy.Width.Should().Be(3);
        }
        [Test]
        public void Height_Is_Set_Correctly_After_Construction()
        {
            new Board(new GameConfiguration {BoardWidth = 3, BoardHeight = 4}).Height.Should().Be(4);
        }
        [Test]
        public void Indexer_Set_Sets_Stone()
        {
            var underTest =
                new Board(new GameConfiguration {BoardWidth = 3, BoardHeight = 4})
                {
                    [2, 1] = new Stone(StoneKind.Regular, 1)
                };
            underTest[2, 1].Owner.Should().Be(1);
        }
        [Test]
        public void Stones_Are_Set_Correctly_After_Construction()
        {
            var underTest = new Board(new GameConfiguration {BoardWidth = 3, BoardHeight = 4});
            for (var x = 0; x < 3; ++x)
            for (var y = 0; y < 4; ++y)
                underTest[x, y].Should().BeEquivalentTo(Stone.Empty);
        }
        [Test]
        public void Width_Is_Set_Correctly_After_Construction()
        {
            new Board(new GameConfiguration {BoardWidth = 3, BoardHeight = 4}).Width.Should().Be(3);
        }
    }
}