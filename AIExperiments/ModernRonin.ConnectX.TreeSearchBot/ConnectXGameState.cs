using System;
using System.Collections.Generic;

namespace ModernRonin.ConnectX.TreeSearchBot
{
    public class ConnectXGameState : IGameState<Move>
    {
        readonly Game mGame;
        readonly IRuleBook mRules;
        public ConnectXGameState(IRuleBook rules, Game game)
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
                    case GameResult.Victory: return 100;
                    case GameResult.Defeat: return -100;
                    case GameResult.Draw: return 0;
                    case GameResult.Undecided: return -1;
                }

                throw new ArgumentOutOfRangeException();
            }
        }
        public IGameState<Move> Execute(Move move)
        {
            var copy = new Game(mGame);
            copy.Execute(move);
            return new ConnectXGameState(mRules, copy);
        }
    }
}