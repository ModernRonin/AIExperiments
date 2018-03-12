using System.Collections.Generic;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace ModernRonin.ConnectX.TreeSearchBot.Tests
{
    [TestFixture]
    public class ConnectXGameStateTests
    {
        [SetUp]
        public void Setup()
        {
            mRules = Substitute.For<IRuleBook>();
            mGame = Substitute.For<IGame>();
            mUnderTest = new ConnectXGameState(mRules, mGame);
        }
        IRuleBook mRules;
        IGame mGame;
        ConnectXGameState mUnderTest;
        [TestCase(GameResult.Victory, 100)]
        [TestCase(GameResult.Defeat, -100)]
        [TestCase(GameResult.Draw, 0)]
        [TestCase(GameResult.Undecided, -1)]
        public void Evaluation_Returns_Score_Depending_On_RulesResult(GameResult result, int expected)
        {
            mRules.Result.Returns(result);

            mUnderTest.Evaluation.Should().Be(expected);
        }
        [Test]
        public void Execute_Returns_GameState_With_Game_That_Has_Executed_Move()
        {
            var gameCopy = Substitute.For<IGame>();
            mGame.Clone().Returns(gameCopy);
            mUnderTest.Execute(new Move(5));

            mGame.DidNotReceiveWithAnyArgs().Execute(new Move());
            gameCopy.Received().Execute(new Move(5));
        }
        [Test]
        public void Execute_Returns_GameState_With_Same_Rules()
        {
            mRules.Result.Returns(GameResult.Victory);
            var newState = mUnderTest.Execute(new Move());

            newState.Evaluation.Should().Be(100);
        }
        [Test]
        public void Execute_Returns_New_GameState()
        {
            mUnderTest.Execute(new Move()).Should().NotBeSameAs(mUnderTest);
        }
        [Test]
        public void LegalMoves_Delegates_To_Rules()
        {
            // ReSharper disable once CollectionNeverUpdated.Local
            var moves = new List<Move>();
            mRules.LegalMoves.Returns(moves);

            mUnderTest.LegalMoves.Should().BeSameAs(moves);
        }
    }
}