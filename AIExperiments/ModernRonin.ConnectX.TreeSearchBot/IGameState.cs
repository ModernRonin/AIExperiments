using System.Collections.Generic;

namespace ModernRonin.ConnectX.TreeSearchBot
{
    public interface IGameState<TMove>
    {
        IEnumerable<TMove> LegalMoves { get; }
        int Evaluation { get; }
        int UniqueHash { get; }
        IGameState<TMove> Execute(TMove move);
    }
}