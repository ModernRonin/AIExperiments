using System;
using System.Linq;
using NUnit.Framework;

namespace ModernRonin.ConnectX.Tests
{
    [TestFixture]
    public class RuleBookTests
    {
        void Set(Board board, string input)
        {
            var rows = input.Split(new[] {' ', '\r', '\n', '\t'}, StringSplitOptions.RemoveEmptyEntries).Reverse().ToArray();

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
                board[x, y] = new Stone {Owner = map(rows[y][x])};
            }

            7.By(6).Do(set);
        }
        [Test]
        public void ResultFor_Discovers_Victory()
        {
            var board = @".......
                          .......
                          x...x.o
                          x...o.o
                          x...o.o
                          x...o.x";

            var game = new Game(GameConfiguration.Default);
        }
    }
}