using System.Collections.Generic;
using System.Linq;

namespace ModernRonin.ConnectX
{
    public class Game
    {
        readonly List<Stone>[] mRemainingStones = {new List<Stone>(), new List<Stone>()};
        public Game(GameConfiguration configuration)
        {
            Board = new Board(configuration);
            2.Do(playerId =>
            {
                mRemainingStones[playerId]
                    .AddRange(configuration.InitialStonesPerPlayer.Select(k => new Stone(k, playerId)));
            });

            PlayerToMove = 0;
        }
        public Game(Game rhs)
        {
            Board = new Board(rhs.Board);
            2.Do(playerId => { mRemainingStones[playerId].AddRange(rhs.mRemainingStones[playerId]); });
            PlayerToMove = rhs.PlayerToMove;
        }
        public Board Board { get; }
        public int PlayerToMove { get; private set; }
        public IEnumerable<Stone> RemainingStones(int playerId) => mRemainingStones[playerId];
        public void Execute(Move move)
        {
            // TODO: make this implementation dependent on rulebook
            mRemainingStones[PlayerToMove].RemoveAt(0);
            var setYCoordinatesinColumn =
                Enumerable.Range(0, Board.Height).Where(i => Board[move.X, i].Owner > -1).ToArray();
            var y = setYCoordinatesinColumn.Any() ? setYCoordinatesinColumn.Max() + 1 : 0;
            Board[move.X, y] = new Stone(move.StoneKind, PlayerToMove);
            PlayerToMove = 0 == PlayerToMove ? 1 : 0;
        }
    }
}