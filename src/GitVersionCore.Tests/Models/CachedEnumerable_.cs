using System;
using System.Linq;
using GitVersion.Models;
using NUnit.Framework;
using Shouldly;

namespace GitVersionCore.Tests.Models
{
    public class CachedEnumerable_
    {
        private Random _random = new Random();

        // This just demonstrates the fact that an enumeration enumerated twice actually performs the enumeration twice.
        [Test]
        public void Enumerating_Underlying_Twice_Actually_Enumerates_Twice()
        {
            var items = Enumerable.Range(1, 100).Select(i => _random.Next());

            var enumerated1 = items.ToList();
            var enumerated2 = items.ToList();

            // The enumeration should have happened twice, and since there is a random number generated for eacn item
            // for each enumeration, the collections should not be the same.
            for (var i = 0; i < items.Count(); i++)
            {
                enumerated1[i].ShouldNotBe(enumerated2[i]);
            }
        }

        [Test]
        public void Returns_The_Same_Elements_As_Underlying_Enumerator()
        {
            // Need to run the ToList() here, to avoid running the random twice.
            var items = Enumerable.Range(1, 100).Select(i => _random.Next()).ToList();
            var cached = items.Cached();

            var enumeratedCached = cached.ToList();
            var enumerated = items.ToList();

            enumeratedCached.ShouldBeEquivalentTo(enumerated);
        }


        [Test]
        public void Enumerates_Underlying_Only_Once_If_Enumerated_Twice()
        {
            // Here we don't enumerate to a list, so if it were no cache, the randoms should be evaluated for each
            // enumeration. Since the collections are equal, the results of the first enumeration are cached.
            var items = Enumerable.Range(1, 100).Select(i => _random.Next());
            var cached = items.Cached();

            var enumerated1 = cached.ToList();
            var enumerated2 = cached.ToList();

            enumerated1.ShouldBeEquivalentTo(enumerated2);
        }
    }
}
