using System;
using System.Linq;

namespace ModernRonin.ConnectX.ConsoleGame
{
    public class HumanConsolePlayer : IPlayer
    {
        public Move GetMove(RuleBook rules, Game game)
        {
            var isValid = false;
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            while (!isValid)
            {
                Console.Write($"Enter a move for player {game.PlayerToMove} [0..6]");
                var input = Console.ReadLine();
                isValid = int.TryParse(input, out var x);
                if (isValid) isValid = rules.LegalMoves().Any(m => m.X == x);
                if (isValid) return new Move(x);
                Console.Error.WriteLine($"{input} is not a valid move!");
            }

            throw new InvalidOperationException();
        }
    }
}