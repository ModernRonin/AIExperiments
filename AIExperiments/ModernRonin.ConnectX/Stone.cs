namespace ModernRonin.ConnectX
{
    public struct Stone
    {
        public Stone(StoneKind kind, int owner)
        {
            Kind = kind;
            Owner = owner;
        }
        public StoneKind Kind { get; }
        public int Owner { get; }
        public static Stone Empty => new Stone( StoneKind.Regular, -1);
    }
}