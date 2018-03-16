using System;
using System.Collections.Generic;
using System.Linq;

namespace ModernRonin.ConnectX.TreeSearchBot
{
    public static class TreeSearch
    {
        static (int, TMove[]) StaticEvaluation<TMove>(IGameState<TMove> startState, int sign) =>
            (sign * startState.Evaluation, new TMove[0]);
        public static (int, IEnumerable<TMove>) NegaMax<TMove>(
            IGameState<TMove> startState,
            int maxDepth,
            int evaluationSign = 1)
        {
            (int, TMove[]) staticEvaluation() => StaticEvaluation(startState, evaluationSign);

            if (0 == maxDepth) return staticEvaluation();
            var bestEval = int.MinValue;
            List<TMove> bestLine = null;
            foreach (var move in startState.LegalMoves)
            {
                var newBoard = startState.Execute(move);
                var (eval, line) = NegaMax(newBoard, maxDepth - 1, -evaluationSign);
                eval *= -1;
                if (eval > bestEval)
                {
                    bestEval = eval;
                    bestLine = line.ToList();
                    bestLine.Insert(0, move);
                }
            }

            if (bestLine == null) return staticEvaluation();

            return (bestEval, bestLine);
        }
        public static (int, IEnumerable<TMove>) AlphaBetaNegaMax<TMove>(
            IGameState<TMove> startState,
            int maxDepth,
            Func<IEnumerable<TMove>, IOrderedEnumerable<TMove>> moveSorter,
            int alpha = int.MinValue,
            int beta = int.MaxValue,
            int evaluationSign = 1)
        {
            (int, TMove[]) staticEvaluation() => StaticEvaluation(startState, evaluationSign);

            if (0 == maxDepth) return staticEvaluation();
            var bestEval = int.MinValue;
            List<TMove> bestLine = null;
            foreach (var move in moveSorter(startState.LegalMoves))
            {
                var newBoard = startState.Execute(move);
                var (eval, line) = AlphaBetaNegaMax(newBoard, maxDepth - 1, moveSorter, -alpha, -beta - evaluationSign);
                eval *= -1;
                if (eval > bestEval)
                {
                    bestEval = eval;
                    bestLine = line.ToList();
                    bestLine.Insert(0, move);
                }

                alpha = Math.Max(alpha, bestEval);
                if (alpha >= beta) break;
            }

            if (bestLine == null) return staticEvaluation();

            return (bestEval, bestLine);
        }
    }
}