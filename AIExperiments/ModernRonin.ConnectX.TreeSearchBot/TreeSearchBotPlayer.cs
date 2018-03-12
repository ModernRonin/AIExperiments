using System;
using System.Collections.Generic;

namespace ModernRonin.ConnectX.TreeSearchBot
{
    public class ConnectXGameState : IGameState<Move>
    {
        readonly RuleBook mRules;
        readonly Game mGame;
        public ConnectXGameState(RuleBook rules, Game game)
        {
            mRules = rules;
            mGame = game;
        }
        public IEnumerable<Move> LegalMoves => mRules.LegalMoves;
        public int Evaluation
        {
            get
            {
                switch (mRules.Result)
                {
                }
                return 0;

            }
        }
        public IGameState<Move> Execute(Move move)
        {
            var copy= new Game(mGame);
            copy.Execute(move);
            return new ConnectXGameState(mRules, copy);
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
            return new Move();
        }
    }
}