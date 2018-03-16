using System;
using System.Collections.Generic;

namespace ModernRonin.ConnectX.TreeSearchBot
{
    public interface ICache<TMove>
    {
        (int, IEnumerable<TMove>) Lookup(
            IGameState<TMove> gameState,
            Func<IGameState<TMove>, (int, IEnumerable<TMove>)> producer);
    }
}