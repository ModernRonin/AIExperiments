using System;
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
            #region Implementing IGameState
            public IEnumerable<char> LegalMoves => mMoves.Keys;
            public int Evaluation { get; private set; }
            public IGameState<char> Execute(char move) => mMoves[move];
            #endregion
        }

        static IEnumerable<Func<IGameState<char>, int, (int, IEnumerable<char>)>> SearchMethods
        {
            get
            {
                yield return (s, d) => TreeSearch.NegaMax(s, d);
            }
        }
        /* 0                            pick maximum
         * 1    A10     B9      C13     eval from player 0's view - evaluation
         *
         * ==> [13, C]
         */
        [Test]
        [TestCaseSource(nameof(SearchMethods))]
        public void NegaMax_OnePly(Func<IGameState<char>, int, (int, IEnumerable<char>)> searchMethod)
        {
            var startState = new GameState {["A"] = 10, ["B"] = 9, ["C"] = 13};

            var (bestEval, bestLine) = searchMethod(startState, 1);
            bestEval.Should().Be(13);
            bestLine.Should().Equal('C');
        }
        /* 0                                                        pick maximum   
         * 1        A5           B11             C9                 eval from player 0's view - pick minimum
         * 2    Aa10  Ab5   Ba11    Bb13    Ca9     Cb17            eval from player 1's view - evaluation
         * ==> [11, Ba]
         */
        [Test]
        public void NegaMax_TwoPlies()
        {
            var startState = new GameState {["Aa"] = 10, ["Ab"] = 5, ["Ba"] = 11, ["Bb"] = 13, ["Ca"] = 9, ["Cb"] = 17};
            var (bestEval, bestLine) = TreeSearch.NegaMax(startState, 2);
            bestEval.Should().Be(11);
            bestLine.Should().Equal('B', 'a');
        }
        /* 0                                                                        pick maximum
         * 1            A7                                  B6                      eval from player 0's view - pick minimum
         * 2     Aa7             Ab9              Ba10                 Bb6          eval from player 1's view - pick maximum
         * 3 AaA7   AaB4     AbA8  AbB9    BaA2   BaB8   BaC10     BbA6   BbB4      eval from player 0's view - evaluation
         * ==> [7, AaA]
         */
        [Test]
        public void NegaMax_ThreePlies()
        {
            var startState= new GameState(){["AaA"]=7, ["AaB"]=4, ["AbA"]=8, ["AbB"]=9,["BaA"]=2, ["BaB"]=8, ["BaC"]=10, ["BbA"]=6, ["BbB"]=4};
            var (bestEval, bestLine) = TreeSearch.NegaMax(startState, 3);
            bestEval.Should().Be(7);
            bestLine.Should().Equal('A', 'a', 'A');
        }
        [Test]
        public void NegaMax_Returns_Evaluation_If_No_LegalMoves_Available
            ()
        {
            var startState= new GameState(){[""]=13};
            var (bestEval, bestLine) = TreeSearch.NegaMax(startState, 3);
            bestEval.Should().Be(13);
            bestLine.Should().BeEmpty();
        }
    }
}