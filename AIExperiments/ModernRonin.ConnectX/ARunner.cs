namespace ModernRonin.ConnectX
{
    public abstract class ARunner
    {
        protected ARunner(Game game)
        {
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
                var move = GetMove();
                Game.Execute(move);
                Render();
                result = Rules.ResultFor();
            }
            while (result == GameResult.Undecided);

            UseResult(result);
        }
        protected abstract void UseResult(GameResult result);
        protected abstract void Render();
        protected abstract Move GetMove();
    }
}