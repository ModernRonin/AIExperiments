using System;
using System.Linq;

namespace ModernRonin.ConnectX
{
    public class HumanConsolePlayer : IPlayer
    {
        public Move GetMove(int playerToMove, Board board, Move[] legalMoves)
        {
            var isValid = false;
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            while (!isValid)
            {
                Console.Write($"Enter a move for player {playerToMove} [0..6]");
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