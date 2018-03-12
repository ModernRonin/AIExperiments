using System.Collections.Generic;

namespace ModernRonin.ConnectX
{
    public interface IRuleBook
    {
        IEnumerable<Move> LegalMoves { get; }
        GameResult Result { get; }
        IRuleBook With(IGame game);
    }
}