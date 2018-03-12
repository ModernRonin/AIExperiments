using System.Collections.Generic;
using System.Linq;

namespace ModernRonin.ConnectX
{
    public class RuleBook : IRuleBook
    {
        RuleBook(IGame game, IEnumerable<Coordinate[]> winnerTuples)
        {
            Game = game;
            WinnerTuples = winnerTuples;
        }
        public RuleBook(IGame game) : this(game,
            WinningTuplesCalculator.WinningTuples(game.Board.Width, game.Board.Height)) { }
        public IGame Game { get; }
        public IEnumerable<Coordinate[]> WinnerTuples { get; }
        public IEnumerable<Move> LegalMoves
        {
            get
            {
                var board = Game.Board;

                bool isColumnFree(int x) => Enumerable.Range(0, board.Height).Any(y => board[x, y].Owner == -1);

                var validX = Enumerable.Range(0, board.Width).Where(isColumnFree);
                return validX.Select(x => new Move(x)).ToArray();
            }
        }
        public GameResult Result
        {
            get
            {
                var victoryOwner = Game.PlayerToMove;
                var defeatOwner = 1 - victoryOwner;
                var winners = WinnerTuples.Select(GetOwnerOf).Where(o => o > -1).ToArray();
                if (winners.Any(o => o == defeatOwner)) return GameResult.Defeat;
                if (winners.Any(o => o == victoryOwner)) return GameResult.Victory;
                if (!LegalMoves.Any()) return GameResult.Draw;
                return GameResult.Undecided;
            }
        }
        public IRuleBook With(IGame game) => new RuleBook(game, WinnerTuples);
        int GetOwnerOf(Coordinate[] tuple)
        {
            var owners = tuple.Select(t => Game.Board[t.X, t.Y].Owner).Distinct().ToArray();
            if (owners.Length > 1) return -1;
            return owners.Single();
        }
    }
}