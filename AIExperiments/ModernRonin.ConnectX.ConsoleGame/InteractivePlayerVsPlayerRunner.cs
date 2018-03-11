using System;
using System.Linq;

namespace ModernRonin.ConnectX.ConsoleGame
{
    public class InteractivePlayerVsPlayerRunner : AConsoleRunner
    {
        public InteractivePlayerVsPlayerRunner(Game game) : base(game) { }
        protected override Move GetMove()
        {
            var isValid = false;
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
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