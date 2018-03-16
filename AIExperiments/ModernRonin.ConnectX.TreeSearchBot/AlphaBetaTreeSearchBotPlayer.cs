using System.Linq;

namespace ModernRonin.ConnectX.TreeSearchBot
{
    public class AlphaBetaTreeSearchBotPlayer : IPlayer
    {
        readonly int mMaxDepth;
        public AlphaBetaTreeSearchBotPlayer(int maxDepth) => mMaxDepth = maxDepth;
        public Move GetMove(IRuleBook rules, IGame game)
        {
            var (_, bestLine) = TreeSearch.AlphaBetaNegaMax(new ConnectXGameState(rules, game), mMaxDepth);
            var result = bestLine.First();
            return result;
        }
    }
}