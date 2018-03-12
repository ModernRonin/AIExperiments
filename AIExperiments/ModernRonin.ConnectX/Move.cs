namespace ModernRonin.ConnectX
{
    public struct Move
    {
        public Move(int x, StoneKind stoneKind = StoneKind.Regular)
        {
            X = x;
            StoneKind = stoneKind;
        }
        public int X { get;  }
        public StoneKind StoneKind { get;  }
    }
}