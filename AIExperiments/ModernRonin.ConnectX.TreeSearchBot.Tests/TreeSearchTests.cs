using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace ModernRonin.ConnectX.TreeSearchBot.Tests
{
    [TestFixture]
    public class TreeSearchTests
    {
        class GameState : IGameState<char>
        {
            #region Implementing IGameState
            public IEnumerable<char> LegalMoves => mMoves.Keys;
            public int Evaluation { get; private set; }
            public IGameState<char> Execute(char move) => mMoves[move];
            #endregion
            readonly Dictionary<char, GameState> mMoves = new Dictionary<char, GameState>();
            public int this[string line]
            {
                get => line.Aggregate(this, (s, m) => s.AfterMove(m)).Evaluation;
                set => line.Aggregate(this, (s, m) => s.AfterMove(m)).Evaluation = value;
            }
            public GameState AfterMove(char move)
            {
                if (mMoves.ContainsKey(move)) return mMoves[move];
                var result = new GameState();
                mMoves[move] = result;
                return result;
            }
            public void EvaluatesAs(int evaluation) => Evaluation = evaluation;
        }

        /* 0
         * 1    A10     B9      C13
         *
         * ==> [13, C]
         */
        [Test]
        public void OnePly()
        {
            var startState = new GameState {["A"] = 10, ["B"] = 9, ["C"] = 13};

            var (bestEval, bestLine) = TreeSearch.NegaMax(startState, 1);
            bestEval.Should().Be(13);
            bestLine.Should().Equal('C');
        }
        /* 0
         * 1        A5           B11             C9                 eval from player 0's view
         * 2    Aa10  Aa5   Bb11    Bb13    Cc9     Cc17            eval from player 1's view
         * ==> [11, Bb]
         */
        [Test]
        public void TwoPlies()
        {
            var startState = new GameState();
            //startState["Aa"]= 
        }
    }
}