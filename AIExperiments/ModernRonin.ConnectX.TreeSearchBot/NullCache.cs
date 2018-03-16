using System;
using System.Collections.Generic;

namespace ModernRonin.ConnectX.TreeSearchBot
{
    public class NullCache<TMove> : ICache<TMove>
    {
        public (int, IEnumerable<TMove>) Lookup(
            IGameState<TMove> gameState,
            Func<IGameState<TMove>, (int, IEnumerable<TMove>)> producer) =>
            producer(gameState);
    }
}