using System.Collections.Generic;

namespace ModernRonin.ConnectX.Tests
{
    static class TestCaseData
    {
        public static IEnumerable<string> VictoryCases
        {
            get
            {
                yield return @".......
                               .......
                               x...x.o
                               x...o.o
                               x...o.o
                               x...o.x";
            }
        }
        public static IEnumerable<string> DefeatCases
        {
            get
            {
                yield return @".......
                               .......
                               ....xox
                               x...oxo
                               x..ooxo
                               x.oxoxx";
            }
        }
        public static IEnumerable<string> DrawCases
        {
            get
            {
                yield return @"xxxooox
                               ooooxxx
                               ooxoxox
                               xxoooxo
                               xoxxoxo
                               xxoxoxx";
            }
        }
        public static IEnumerable<string> UndecidedCases
        {
            get
            {
                yield return @"....x..
                               ....x..
                               ....xox
                               x...oxo
                               x..ooxo
                               x.oxoxx";
            }
        }
    }
}