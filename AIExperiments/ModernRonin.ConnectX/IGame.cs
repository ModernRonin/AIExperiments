using System.Collections.Generic;

namespace ModernRonin.ConnectX
{
    public interface IGame
    {
        Board Board { get; }
        int PlayerToMove { get; }
        IEnumerable<Stone> RemainingStones(int playerId);
        void Execute(Move move);
        IGame Clone();
    }
}