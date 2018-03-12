using System.Collections.Generic;
using System.Linq;

namespace ModernRonin.ConnectX.TreeSearchBot
{
    public static class TreeSearch
    {
        public static (int, IEnumerable<TMove>) NegaMax<TMove>(IGameState<TMove> startState, int maxDepth)
        {
            if (0 == maxDepth) return (startState.Evaluation, new TMove[0]);
            var bestEval = int.MinValue;
            List<TMove> bestLine = null;
            foreach (var move in startState.LegalMoves)
            {
                var newBoard = startState.Execute(move);
                var (eval, line) = NegaMax(newBoard, maxDepth - 1);
                if (eval > bestEval)
                {
                    bestEval = eval;
                    bestLine = line.ToList();
                    bestLine.Insert(0, move);
                }
            }

            return (bestEval, bestLine);
        }
    }
}