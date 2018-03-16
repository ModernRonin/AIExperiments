using System;
using System.Linq;
using System.Text;

namespace ModernRonin.ConnectX.ConsoleGame
{
    public class ConsoleRunner : ARunner
    {
        public ConsoleRunner(Game game, IPlayer[] players) : base(game, players) { }
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
            Console.Write("Press <Enter> to continue...");
            Console.ReadLine();
        }
        protected override void UseTimeTakenForMove(int playerToMove, TimeSpan deltaTime)
        {
            if (1==playerToMove)
            {
                var milliseconds = (int) Math.Round(deltaTime.TotalMilliseconds);
                Console.WriteLine($"Bot took {milliseconds}ms for move");
            }
        }
    }
}