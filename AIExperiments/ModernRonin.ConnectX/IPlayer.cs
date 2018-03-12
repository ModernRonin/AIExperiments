namespace ModernRonin.ConnectX
{
    public interface IPlayer
    {
        Move GetMove(IRuleBook rules, Game game);
    }
}