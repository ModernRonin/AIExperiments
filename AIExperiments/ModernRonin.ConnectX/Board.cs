namespace ModernRonin.ConnectX {
    public class Board
    {
        readonly Stone[,] mStones;
        public Board(GameConfiguration configuration)
        {
            mStones= new Stone[configuration.BoardWidth,configuration.BoardHeight];
        }
        public Stone this[int x, int y]
        {
            get { return mStones[x, y]; }
        }
    }
}