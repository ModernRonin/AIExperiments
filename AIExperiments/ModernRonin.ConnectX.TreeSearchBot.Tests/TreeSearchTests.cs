using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ModernRonin.ConnectX.TreeSearchBot.Tests
{
    [TestFixture]
    public class TreeSearchTests
    {
        class GameState : IGameState<char>, IEnumerable<GameState>
        {
            readonly Dictionary<char, GameState> mMoves = new Dictionary<char, GameState>();
            public string State { get; set; }
            public IEnumerator<GameState> GetEnumerator() => mMoves.Values.GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            public IEnumerable<char> LegalMoves => mMoves.Keys;
            public int Evaluation { get; set; }
            public IGameState<char> Execute(char move) => mMoves[move];
            public GameState AfterMove(char move)
            {
                if (mMoves.ContainsKey(move)) return mMoves[move];
                var result = new GameState {State = State + move};
                mMoves[move] = result;
                return result;
            }
            public void EvaluatesAs(int evaluation) => Evaluation = evaluation;
            public void Add(GameState child)
            {
                var move = child.State.Last();
                mMoves[move] = child;
            }
        }

        /* 0
         * 1    A10     B9      C13
         *
         * ==> [13, C]
         */
        [Test]
        public void OnePly()
        {
            var startState = new GameState();
            startState.AfterMove('A').EvaluatesAs(10);
            startState.AfterMove('B').EvaluatesAs(9);
            startState.AfterMove('C').EvaluatesAs(13);

        }
    }
}