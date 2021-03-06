﻿using System;
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
            int evaluationSign = 1) =>
            NegaMax(startState, maxDepth, new NullCache<TMove>(), evaluationSign);
        public static (int, IEnumerable<TMove>) NegaMax<TMove>(
            IGameState<TMove> startState,
            int maxDepth,
            ICache<TMove> cache,
            int evaluationSign = 1)
        {
            (int, TMove[]) staticEvaluation() => StaticEvaluation(startState, evaluationSign);

            if (0 == maxDepth) return staticEvaluation();
            var bestEval = int.MinValue;
            List<TMove> bestLine = null;
            foreach (var move in startState.LegalMoves)
            {
                var newBoard = startState.Execute(move);
                var (eval, line) = cache.Lookup(newBoard, b => NegaMax(b, maxDepth - 1, cache, -evaluationSign));
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
        /// <summary>
        /// Do not call with int.MinValue for alpha, lest you get an overflow error:
        ///  (int.MinValue* (-1)) == int.MinValue in C#
        /// </summary>
        /// <typeparam name="TMove"></typeparam>
        /// <param name="startState"></param>
        /// <param name="maxDepth"></param>
        /// <param name="alpha"></param>
        /// <param name="beta"></param>
        /// <param name="evaluationSign"></param>
        /// <returns></returns>
        public static (int, IEnumerable<TMove>) AlphaBetaNegaMax<TMove>(
            IGameState<TMove> startState,
            int maxDepth,
            int alpha = int.MinValue + 1,
            int beta = int.MaxValue,
            int evaluationSign = 1)
        {
            return AlphaBetaNegaMax(startState, maxDepth, _ => _, alpha, beta, evaluationSign);
        }
        /// <summary>
        /// Do not call with int.MinValue for alpha, lest you get an overflow error:
        ///  (int.MinValue* (-1)) == int.MinValue in C#
        /// </summary>
        /// <typeparam name="TMove"></typeparam>
        /// <param name="startState"></param>
        /// <param name="maxDepth"></param>
        /// <param name="alpha"></param>
        /// <param name="beta"></param>
        /// <param name="evaluationSign"></param>
        /// <returns></returns>
        public static (int, IEnumerable<TMove>) AlphaBetaNegaMax<TMove>(
            IGameState<TMove> startState,
            int maxDepth,
            Func<IEnumerable<TMove>, IEnumerable<TMove>> moveSorter,
            int alpha = int.MinValue + 1,
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
                var (eval, line) = AlphaBetaNegaMax(newBoard, maxDepth - 1, moveSorter, -beta, -alpha, -evaluationSign);
                eval *= -1;
                if (eval > bestEval)
                {
                    bestEval = eval;
                    bestLine = line.ToList();
                    bestLine.Insert(0, move);
                }

                if (eval > alpha) alpha = eval;
                if (alpha >= beta) break;
            }

            if (bestLine == null) return staticEvaluation();

            return (bestEval, bestLine);
        }
    }
}