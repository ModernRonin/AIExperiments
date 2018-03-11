namespace ModernRonin.ConnectX
{
    public class Stone
    {
        public StoneKind Kind { get; set; }
        public int Owner { get; set; }
        public static Stone Empty => new Stone {Kind = StoneKind.Regular, Owner = -1};
    }
}