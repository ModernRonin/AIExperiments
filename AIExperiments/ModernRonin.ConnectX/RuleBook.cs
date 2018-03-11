using System.Collections.Generic;
using System.Linq;

namespace ModernRonin.ConnectX
{
    public class WinningTuplesCalculator
    {
        static IEnumerable<Coordinate[]> WinningHorizontals(int width, int height)
        {
            Coordinate[] rightFrom(int y, int start) =>
                Enumerable.Range(0, 4).Select(x => new Coordinate(x + start, y)).ToArray();

            for (var y = 0; y < height; ++y)
            for (var start = 0; start <= width - 4; ++start)
                yield return rightFrom(y, start);
        }
        static IEnumerable<Coordinate[]> WinningVerticals(int width, int height)
        {
            Coordinate[] upFrom(int x, int start) => Enumerable.Range(0, 4).Select(y => new Coordinate(x, y + start)).ToArray();

            for (var x= 0; x<width; ++x)
            for (var start = 0; start <= height-4; ++start)
                yield return upFrom(x, start);
        }
        static IEnumerable<Coordinate[]> WinningDiagonalsFromLeftBottom(int width, int height)
        {
            Coordinate[] from(int startX, int startY) =>
                Enumerable.Range(0, 4).Select(o => new Coordinate(startX + o, startY + o)).ToArray();

            for (var x=0; x<=width-4; ++x)
            for (var y = 0; y <= height - 4; ++y)
                yield return @from(x, y);
        }
        static IEnumerable<Coordinate[]> WinningDiagonalsFromLeftTop(int width, int height)
        {
            Coordinate[] from(int startX, int startY) =>
                Enumerable.Range(0, 4).Select(o => new Coordinate(startX + o, startY - o)).ToArray();

            for (var x = 0; x <= width - 4; ++x)
            for (var y = 3; y < height; ++y)
                yield return @from(x, y);
        }
        public static IEnumerable<Coordinate[]> WinningTuples(int width, int height) =>
            WinningHorizontals(width, height).Concat(WinningVerticals(width, height))
                                             .Concat(WinningDiagonalsFromLeftBottom(width, height))
                                             .Concat(WinningDiagonalsFromLeftTop(width, height));
    }
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

    public struct Coordinate
    {
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X { get; }
        public int Y { get; }
        public override string ToString() => $"({X}/{Y})";
    }
}