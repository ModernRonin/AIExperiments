using System;

namespace ModernRonin.ConnectX
{
    public struct Stone
    {
        static readonly int sNumberOfStoneKinds= Enum.GetNames(typeof(StoneKind)).Length+1;
        public Stone(StoneKind kind, int owner)
        {
            Kind = kind;
            Owner = owner;
        }
        public StoneKind Kind { get; }
        public int Owner { get; }
        public static Stone Empty => new Stone( StoneKind.Regular, -1);
        public int UniqueHash
        {
            get
            {
                
                var ownerPlace = (Owner + 1)*sNumberOfStoneKinds;
                var kindPlace = (1+(int)Kind);
                return ownerPlace + kindPlace;
            }
        }
    }
}