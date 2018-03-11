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
            for (var playerId = 0; playerId < 2; ++playerId)
            {
                mRemainingStones[playerId]
                    .AddRange(configuration.InitialStonesPerPlayer.Select(k => new Stone {Kind = k, Owner = playerId}));
            }

            PlayerToMoveId = 0;
        }
        public Board Board { get; set; }
        public int PlayerToMoveId { get; private set; }
        public IEnumerable<Stone> RemainingStones(int playerId) => mRemainingStones[playerId];
        public void Execute(Move move)
        {
            // TODO: make this implementation dependent on rulebook
            mRemainingStones[PlayerToMoveId].RemoveAt(0);
            var y = Enumerable.Range(0, Board.Height).Where(i => Board[move.X, i].Owner < 0).Max() + 1;
            Board[move.X, y] = new Stone {Owner = PlayerToMoveId, Kind = move.StoneKind};
            PlayerToMoveId = 0 == PlayerToMoveId ? 1 : 0;
        }
    }
}