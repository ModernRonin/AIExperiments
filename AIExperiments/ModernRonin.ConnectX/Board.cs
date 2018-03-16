using System;

namespace ModernRonin.ConnectX
{
    public class Board
    {
        readonly Stone[,] mStones;
        public Board(GameConfiguration configuration)
        {
            mStones = new Stone[configuration.BoardWidth, configuration.BoardHeight];
            configuration.BoardWidth.By(configuration.BoardHeight).Do((x, y) => mStones[x, y] = Stone.Empty);
        }
        public Board(Board rhs)
        {
            mStones = new Stone[rhs.Width, rhs.Height];
            Array.Copy(rhs.mStones, mStones, rhs.Width * rhs.Height);
        }
        public int Width => mStones.GetUpperBound(0) + 1;
        public int Height => mStones.GetUpperBound(1) + 1;
        public Stone this[int x, int y]
        {
            get => mStones[x, y];
            set => mStones[x, y] = value;
        }
        public int UniqueHash
        {
            get { return 0; }
        }
    }
}