using System;
using System.Linq;
using System.Text;

namespace ModernRonin.ConnectX.ConsoleGame
{
    public abstract class AConsoleRunner : ARunner
    {
        public AConsoleRunner(Game game) : base(game) { }
        protected override void Render()
        {
            var buffer = new StringBuilder();
            for (var y = Game.Board.Height - 1; y >= 0; --y)
            {
                var row = string.Empty;
                for (var x = 0; x < Game.Board.Width; ++x) row += GetSymbolFor(Game.Board[x, y]);

                buffer.AppendLine(row);
            }

            buffer.AppendLine(string.Join("", Enumerable.Range(0, Game.Board.Width).Select(i => i.ToString())));
            Console.WriteLine(buffer);
        }
        static string GetSymbolFor(Stone stone)
        {
            if (0 > stone.Owner) return ".";
            if (0 == stone.Owner) return "x";
            if (1 == stone.Owner) return "o";
            return "?";
        }
        protected override void UseResult(GameResult result)
        {
            if (GameResult.Draw == result) Console.WriteLine("It's a draw");
            else if (GameResult.Defeat == result) Console.WriteLine($"Player {1 - Game.PlayerToMove} has won");
            else Console.WriteLine("Weird result");
        }
    }
}