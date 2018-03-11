namespace ModernRonin.ConnectX
{
    public interface IPlayer
    {
        Move GetMove(int playerToMove, Board board, Move[] legalMoves);
    }
}