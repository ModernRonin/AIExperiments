using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace ModernRonin.ConnectX.Tests
{
    [TestFixture]
    public class StoneTests
    {
        [Test]
        public void UniqueHash_Should_Be_Unique()
        {
            var set = new HashSet<int>();
            var permutationCount = 0;
            foreach (var kind in Enum.GetValues(typeof(StoneKind)).Cast<StoneKind>())
            {
                for (var owner = -1; owner < 2; ++owner)
                {
                    ++permutationCount;
                    set.Add(new Stone(kind, owner).UniqueHash);
                }
            }

            set.Count.Should().Be(permutationCount);
        }
        [Test]
        public void MaxHash()
        {
            Stone.MaxHash.Should().Be(3);
        }
    }
}