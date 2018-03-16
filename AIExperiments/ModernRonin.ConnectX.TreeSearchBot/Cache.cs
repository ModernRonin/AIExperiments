using System;
using System.Collections.Generic;

namespace ModernRonin.ConnectX.TreeSearchBot
{
    public class Cache<TMove> : ICache<TMove>
    {
        readonly Dictionary<string, (int, IEnumerable<TMove>)> mCache;
        public Cache(int capacity) => mCache = new Dictionary<string, (int, IEnumerable<TMove>)>(capacity);
        public (int, IEnumerable<TMove>) Lookup(
            IGameState<TMove> gameState,
            Func<IGameState<TMove>, (int, IEnumerable<TMove>)> producer)
        {
            var key = gameState.UniqueHash;
            if (!mCache.ContainsKey(key)) mCache[key] = producer(gameState);
            return mCache[key];
        }
    }
}