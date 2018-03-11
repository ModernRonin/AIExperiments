﻿using System.Collections.Generic;
using System.Linq;

namespace ModernRonin.ConnectX
{
    public class RuleBook
    {
        readonly Game mGame;
        public RuleBook(Game game)
        {
            mGame = game;
        }
        public IEnumerable<Move> LegalMoves()
        {
            var board = mGame.Board;

            bool isColumnFree(int x) => Enumerable.Range(0, board.Height).Any(y => board[x, y].Owner == -1);

            var validX = Enumerable.Range(0, board.Width).Where(isColumnFree);
            return validX.Select(x => new Move {X = x}).ToArray();
        }
        int GetOwnerOf(Coordinate[] tuple)
        {
            var owners = tuple.Select(t => mGame.Board[t.X, t.Y].Owner).Distinct().ToArray();
            if (owners.Length > 1) return -1;
            return owners.Single();
        }
        public GameResult ResultFor()
        {
            var victoryOwner = mGame.PlayerToMove;
            var defeatOwner = 1 - victoryOwner;
            var winningPossibilities = WinningTuplesCalculator.WinningTuples(mGame.Board.Width, mGame.Board.Height);
            var winners = winningPossibilities.Select(GetOwnerOf).Where(o => o>-1).ToArray();
            if (winners.Any(o => o == defeatOwner)) return GameResult.Defeat;
            if (winners.Any(o => o == victoryOwner)) return GameResult.Victory;
            if (!LegalMoves().Any()) return GameResult.Draw;
            return GameResult.Undecided;
        }
    }
}