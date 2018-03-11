using System.Collections.Generic;
using System.Linq;

namespace ModernRonin.ConnectX
{
    public class GameState
    {
        readonly List<Stone>[] mRemainingStones = new List<Stone>[2];
        public GameState(GameConfiguration configuration)
        {
            Board = new Board(configuration);
            for (var playerId = 0; playerId < 2; ++playerId)
                mRemainingStones[playerId]
                    .AddRange(configuration.InitialStonesPerPlayer.Select(k => new Stone {Kind = k, Owner = playerId}));
            PlayerToMoveId = 0;
        }
        public Board Board { get; }
        public int PlayerToMoveId { get; }
        public IEnumerable<Stone> RemainingStones(int playerId) => mRemainingStones[playerId];
    }
}