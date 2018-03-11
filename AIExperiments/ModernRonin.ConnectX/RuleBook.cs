using System.Collections.Generic;
using System.Linq;

namespace ModernRonin.ConnectX
{
    public class RuleBook
    {
        public IEnumerable<Move> LegalMoves(Game game)
        {
            var board = game.Board;

            bool isColumnFree(int x) => Enumerable.Range(0, board.Height).Any(y => board[x, y].Owner == -1);

            var validX = Enumerable.Range(0, board.Width).Where(isColumnFree);
            return validX.Select(x => new Move {X = x}).ToArray();
        }
        static int GetOwnerOf(Coordinate[] tuple, Board board)
        {
            var owners = tuple.Select(t => board[t.X, t.Y].Owner).Distinct().ToArray();
            if (owners.Length > 1) return -1;
            return owners.Single();
        }
        public GameResult ResultFor(Game game)
        {
            var board = game.Board;
            var victoryOwner = game.PlayerToMove;
            var defeatOwner = 1 - victoryOwner;
            var winningPossibilities = WinningTuplesCalculator.WinningTuples(board.Width, board.Height);
            var winners = winningPossibilities.Select(p => GetOwnerOf(p, board)).Where(o => o>-1).ToArray();
            if (winners.Any(o => o == defeatOwner)) return GameResult.Defeat;
            if (winners.Any(o => o == victoryOwner)) return GameResult.Victory;
            if (!LegalMoves(game).Any()) return GameResult.Draw;
            return GameResult.Undecided;
        }
    }
}