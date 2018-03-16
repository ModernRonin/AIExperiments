using System;
using System.Linq;

namespace ModernRonin.ConnectX.TreeSearchBot
{
    public class TreeSearchBotPlayer : IPlayer
    {
        readonly int mMaxDepth;
        readonly Cache<Move> mCache = new Cache<Move>(7*6*3);
        public TreeSearchBotPlayer(int maxDepth) => mMaxDepth = maxDepth;
        public Move GetMove(IRuleBook rules, IGame game)
        {
            var (_, bestLine) = TreeSearch.NegaMax(new ConnectXGameState(rules, game), mMaxDepth, mCache);
            var result = bestLine.First();
            Console.WriteLine($"Cache: hits={mCache.HitCount}, misses={mCache.MissCount}");
            return result;
        }
    }
}