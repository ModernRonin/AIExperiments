using System;
using System.Linq;

namespace ModernRonin.ConnectX
{
    public interface IPlayer {
        Move GetMove(int playerToMove, Board board, Move[] legalMoves);
    }

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
                if (isValid) return new Move { X = x, StoneKind = StoneKind.Regular };
                Console.Error.WriteLine($"{input} is not a valid move!");
            }

            return null;
        }

    }
    public abstract class ARunner
    {
        readonly IPlayer[] mPlayers;
        protected ARunner(Game game, IPlayer[] players)
        {
            if (2!=players.Length) throw new ArgumentException("Need exactly two players", nameof(players));
            mPlayers = players;
            Game = game;
            Rules = new RuleBook(Game);

        }
        protected Game Game { get; }
        protected RuleBook Rules { get; }
        public void Run()
        {
            Render();
            GameResult result;
            do
            {
                var playerToMove = Game.PlayerToMove;
                var move = mPlayers[playerToMove].GetMove(playerToMove, Game.Board, Rules.LegalMoves().ToArray());
                Game.Execute(move);
                Render();
                result = Rules.ResultFor();
            }
            while (result == GameResult.Undecided);

            UseResult(result);
        }
        protected abstract void UseResult(GameResult result);
        protected abstract void Render();
    }
}