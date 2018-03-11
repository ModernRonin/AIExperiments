using System;
using System.Linq;
using System.Text;

namespace ModernRonin.ConnectX.ConsoleGame
{
    public abstract class ARunner
    {
        protected ARunner(Game game)
        {
            Game = game;
            Rules = new RuleBook(Game);
        }
        protected Game Game { get; }
        protected RuleBook Rules { get; }
    }

    public class InteractiveRunner : ARunner
    {
        public InteractiveRunner(Game game) : base(game) { }
        public void Run()
        {
            Render();
            GameResult result;
            do
            {
                var move = GetMoveFromUser();
                Game.Execute(move);
                Render();
                result = Rules.ResultFor();
            }
            while (result == GameResult.Undecided);

            if (GameResult.Draw == result) Console.WriteLine("It's a draw");
            else if (GameResult.Defeat == result) Console.WriteLine($"Player {1 - Game.PlayerToMove} has won");
            else Console.WriteLine("Weird result");
        }
        void Render()
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
        Move GetMoveFromUser()
        {
            var isValid = false;
            while (!isValid)
            {
                var legalMoves = Rules.LegalMoves().ToArray();
                Console.Write($"Enter a move for player {Game.PlayerToMove} [0..6]");
                var input = Console.ReadLine();
                isValid = int.TryParse(input, out var x);
                if (isValid) isValid = legalMoves.Any(m => m.X == x);
                if (isValid) return new Move {X = x, StoneKind = StoneKind.Regular};
                Console.Error.WriteLine($"{input} is not a valid move!");
            }

            return null;
        }
    }
}