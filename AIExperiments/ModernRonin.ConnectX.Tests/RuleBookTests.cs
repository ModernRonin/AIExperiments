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