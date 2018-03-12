using System;
using System.Collections.Generic;

namespace ModernRonin.ConnectX.TreeSearchBot
{
    public class ConnectXGameState : IGameState<Move>
    {
        public IEnumerable<Move> LegalMoves { get; }
        public int Evaluation { get; }
        public IGameState<Move> Execute(Move move)
        {
            throw new NotImplementedException();
        }
    }
    public class TreeSearchBotPlayer : IPlayer
    {
        readonly int mMaxDepth;
        public TreeSearchBotPlayer(int maxDepth)
        {
            mMaxDepth = maxDepth;
        }
        public Move GetMove(RuleBook rules, Game game)
        {
            //var (eval, bestLine) = TreeSearch.NegaMax()
            return null;
        }
    }
}