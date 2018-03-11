using System.Collections.Generic;
using System.Linq;

namespace ModernRonin.ConnectX
{
    public class GameConfiguration
    {
        public int BoardWidth { get; set; }
        public int BoardHeight { get; set; }
        public IEnumerable<StoneKind> InitialStonesPerPlayer { get; set; }
        public static GameConfiguration Default =>
            new GameConfiguration
            {
                BoardWidth = 7,
                BoardHeight = 6,
                InitialStonesPerPlayer = Enumerable.Repeat(StoneKind.Regular, 7 * 6 / 2).ToArray()
            };
        public override string ToString()
        {
            var initialStones = string.Join(", ",
                InitialStonesPerPlayer.GroupBy(k => k).Select(g => $"{g.Count()}x{g.Key}"));
            return
                $"{nameof(BoardWidth)}: {BoardWidth}\n{nameof(BoardHeight)}: {BoardHeight}\n{nameof(InitialStonesPerPlayer)}: {initialStones}\n";
        }
    }
}