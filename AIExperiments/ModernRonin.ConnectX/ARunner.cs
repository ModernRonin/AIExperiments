using System;

namespace ModernRonin.ConnectX
{
    public abstract class ARunner
    {
        readonly IPlayer[] mPlayers;
        protected ARunner(IGame game, IPlayer[] players)
        {
            if (2 != players.Length) throw new ArgumentException("Need exactly two players", nameof(players));
            mPlayers = players;
            Game = game;
            Rules = new RuleBook(Game);
        }
        protected IGame Game { get; }
        protected RuleBook Rules { get; }
        public void Run()
        {
            Render();
            GameResult result;
            do
            {
                var playerToMove = Game.PlayerToMove;
                Move move;
                using (new StopWatchEx(dt => UseTimeTakenForMove(playerToMove, dt)))
                    move = mPlayers[playerToMove].GetMove(Rules, Game);
                Game.Execute(move);
                Render();
                result = Rules.Result;
            }
            while (result == GameResult.Undecided);

            UseResult(result);
        }
        protected virtual void UseTimeTakenForMove(int playerToMove, TimeSpan deltaTime) { } 
        protected abstract void UseResult(GameResult result);
        protected abstract void Render();
    }
}