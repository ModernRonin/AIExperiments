﻿using System.Collections.Generic;

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
                yield return @".......
                               x......
                               x...x.o
                               x...o.o
                               x...o.o
                               o...o.x";
                yield return @"x......
                               x......
                               x...x.o
                               x...o.o
                               o...o.o
                               o...o.x";
                yield return @".......
                               .......
                               ....ox.
                               ....xo.
                               ...xxo.
                               ..xoxo.";
                yield return @".......
                               .......
                               ..xo...
                               ..ox...
                               ..oxx..
                               ..oxox.";
                yield return @".......
                               .......
                               .......
                               .......
                               xxxxo..
                               xooxox.";
                yield return @".......
                               .......
                               .......
                               .......
                               .xxxxo.
                               xooxox.";
                yield return @".......
                               .......
                               .......
                               .......
                               ..xxxxo
                               xooxox.";
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
        public static IEnumerable<object[]> LegalMovesCases
        {
            get
            {
                yield return new object[]
                {
                    @".......
                      .......
                      .......
                      .......
                      .......
                      .......",
                    new[] {0, 1, 2, 3, 4, 5, 6}
                };
                yield return new object[]
                {
                    @".x.o...
                      .x.o...
                      ox.o...
                      xo.x...
                      xo.x...
                      xo.x...",
                    new[] {0, 2, 4, 5, 6}
                };
                yield return new object[]
                {
                    @".......
                      .......
                      ....xox
                      x...oxo
                      x..ooxo
                      x.oxoxx",
                    new[] {0, 1, 2, 3, 4, 5, 6}
                };
            }
        }
    }
}