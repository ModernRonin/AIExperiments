using System.Collections.Generic;

namespace ModernRonin.ConnectX {
    public class RuleBook
    {
        public IEnumerable<Move> LegalMoves(Board board)
        {
            return null;
        }
        public GameResult ResultFor(Board board)
        {
            return GameResult.Undecided;
        }
    }
}