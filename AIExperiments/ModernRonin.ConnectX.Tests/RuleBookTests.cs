using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace ModernRonin.ConnectX.Tests
{
    [TestFixture]
    public class RuleBookTests
    {
        void Set(Board board, string input)
        {
            var rows = input.Split(new[] {' ', '\r', '\n', '\t'}, StringSplitOptions.RemoveEmptyEntries).Reverse()
                            .ToArray();

            int map(char c)
            {
                switch (c)
                {
                    case '.': return -1;
                    case 'x': return 0;
                    case 'o': return 1;
                }

                throw new IndexOutOfRangeException();
            }

            void set(int x, int y)
            {
                board[x, y] = new Stone(StoneKind.Regular,map(rows[y][x]));
            }

            7.By(6).Do(set);
        }
        Game SetupGame(string board)
        {
            var result = new Game(GameConfiguration.Default);
            Set(result.Board, board);
            return result;
        }
        [Test]
        [TestCaseSource(typeof(TestCaseData), nameof(TestCaseData.DefeatCases))]
        public void ResultFor_Discovers_Defeat(string input)
        {
            var game = SetupGame(input);

            var result = new RuleBook(game).ResultFor();
            Assert.AreEqual(GameResult.Defeat, result);
        }
        [Test]
        [TestCaseSource(typeof(TestCaseData), nameof(TestCaseData.DrawCases))]
        public void ResultFor_Discovers_Draw(string input)
        {
            var game = SetupGame(input);

            var result = new RuleBook(game).ResultFor();
            
            Assert.AreEqual(GameResult.Draw, result);
        }
        [Test]
        [TestCaseSource(typeof(TestCaseData), nameof(TestCaseData.UndecidedCases))]
        public void ResultFor_Discovers_Undecided(string input)
        {
            var game = SetupGame(input);

            var result = new RuleBook(game).ResultFor();
            Assert.AreEqual(GameResult.Undecided, result);
        }
        [Test]
        [TestCaseSource(typeof(TestCaseData), nameof(TestCaseData.VictoryCases))]
        public void ResultFor_Discovers_Victory(string input)
        {
            var game = SetupGame(input);

            var result = new RuleBook(game).ResultFor();
            Assert.AreEqual(GameResult.Victory, result);
        }
        [Test]
        [TestCaseSource(typeof(TestCaseData), nameof(TestCaseData.LegalMovesCases))]
        public void LegalMoves(string boardInput, int[] expectedMoveX)
        {
            var game = SetupGame(boardInput);

            var result= new RuleBook(game).LegalMoves().ToArray();

            result.All(m => m.StoneKind == StoneKind.Regular).Should().BeTrue();
            result.Select(m => m.X).Should().BeEquivalentTo(expectedMoveX);
        }
    }
}