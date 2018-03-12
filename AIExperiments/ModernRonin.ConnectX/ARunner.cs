using System;

namespace ModernRonin.ConnectX
{
    public abstract class ARunner
    {
        readonly IPlayer[] mPlayers;
        protected ARunner(Game game, IPlayer[] players)
        {
            if (2 != players.Length) throw new ArgumentException("Need exactly two players", nameof(players));
            mPlayers = players;
            Game = game;
            Rules = new RuleBook(Game);
        }
        protected Game Game { get; }
        protected RuleBook Rules { get; }
        public void Run()
        {
            Render();
            GameResult result;
            do
            {
                var playerToMove = Game.PlayerToMove;
                var move = mPlayers[playerToMove].GetMove(Rules, Game);
                Game.Execute(move);
                Render();
                result = Rules.ResultFor();
            }
            while (result == GameResult.Undecided);

            UseResult(result);
        }
        protected abstract void UseResult(GameResult result);
        protected abstract void Render();
    }
}