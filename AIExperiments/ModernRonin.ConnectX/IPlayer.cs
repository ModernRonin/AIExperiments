namespace ModernRonin.ConnectX
{
    public interface IPlayer
    {
        Move GetMove(RuleBook rules, Game game);
    }
}