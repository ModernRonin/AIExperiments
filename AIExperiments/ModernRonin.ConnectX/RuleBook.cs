﻿using System.Collections.Generic;
using System.Linq;

namespace ModernRonin.ConnectX
{
    public class RuleBook : IRuleBook
    {
        readonly Game mGame;
        readonly IEnumerable<Coordinate[]> mWinningPossibilities;
        public RuleBook(Game game)
        {
            mGame = game;
            mWinningPossibilities = WinningTuplesCalculator.WinningTuples(mGame.Board.Width, mGame.Board.Height);
        }
        public IEnumerable<Move> LegalMoves
        {
            get
            {
                var board = mGame.Board;

                bool isColumnFree(int x) => Enumerable.Range(0, board.Height).Any(y => board[x, y].Owner == -1);

                var validX = Enumerable.Range(0, board.Width).Where(isColumnFree);
                return validX.Select(x => new Move(x)).ToArray();
            }
        }
        public GameResult Result
        {
            get
            {
                var victoryOwner = mGame.PlayerToMove;
                var defeatOwner = 1 - victoryOwner;
                var winners = mWinningPossibilities.Select(GetOwnerOf).Where(o => o > -1).ToArray();
                if (winners.Any(o => o == defeatOwner)) return GameResult.Defeat;
                if (winners.Any(o => o == victoryOwner)) return GameResult.Victory;
                if (!LegalMoves.Any()) return GameResult.Draw;
                return GameResult.Undecided;
            }
        }
        int GetOwnerOf(Coordinate[] tuple)
        {
            var owners = tuple.Select(t => mGame.Board[t.X, t.Y].Owner).Distinct().ToArray();
            if (owners.Length > 1) return -1;
            return owners.Single();
        }
    }
}