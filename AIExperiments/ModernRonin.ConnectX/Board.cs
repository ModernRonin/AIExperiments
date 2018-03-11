namespace ModernRonin.ConnectX
{
    public class Board
    {
        readonly Stone[,] mStones;
        public Board(GameConfiguration configuration)
        {
            mStones = new Stone[configuration.BoardWidth, configuration.BoardHeight];
            for (var x = 0; x < configuration.BoardWidth; ++x)
            for (var y = 0; y < configuration.BoardHeight; ++y)
                mStones[x, y] = Stone.Empty;
        }
        public int Width => mStones.GetUpperBound(0) + 1;
        public int Height => mStones.GetUpperBound(1) + 1;
        public Stone this[int x, int y]
        {
            get => mStones[x, y];
            set => mStones[x, y] = value;
        }
    }
}