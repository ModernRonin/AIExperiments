﻿namespace ModernRonin.ConnectX
{
    public class Board
    {
        readonly Stone[,] mStones;
        public Board(GameConfiguration configuration) =>
            mStones = new Stone[configuration.BoardWidth, configuration.BoardHeight];
        public int Width => mStones.GetUpperBound(0) + 1;
        public int Height => mStones.GetUpperBound(1) + 1;
        public Stone this[int x, int y] => mStones[x, y];
    }
}