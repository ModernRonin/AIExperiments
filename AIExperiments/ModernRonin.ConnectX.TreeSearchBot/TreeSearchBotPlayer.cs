using System.Linq;

namespace ModernRonin.ConnectX.TreeSearchBot
{
    public class TreeSearchBotPlayer : IPlayer
    {
        readonly int mMaxDepth;
        public TreeSearchBotPlayer(int maxDepth)
        {
            mMaxDepth = maxDepth;
        }
        public Move GetMove(IRuleBook rules, IGame game)
        {
            var (eval, bestLine) = TreeSearch.NegaMax(new ConnectXGameState(rules, game), mMaxDepth);
            return bestLine.First();
        }
    }
}