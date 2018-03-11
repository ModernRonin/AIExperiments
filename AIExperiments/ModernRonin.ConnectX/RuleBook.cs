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
        public GameResult ResultFor(Game game) => GameResult.Undecided;
    }
}