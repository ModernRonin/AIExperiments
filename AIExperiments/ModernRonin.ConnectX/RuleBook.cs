using System.Collections.Generic;

namespace ModernRonin.ConnectX
{
    public class RuleBook
    {
        public IEnumerable<Move> LegalMoves(Game game) => null;
        public GameResult ResultFor(Game game) => GameResult.Undecided;
    }
}