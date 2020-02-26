using System;
using System.Collections.Generic;
using System.Linq;
using GitVersion.Models;
using NUnit.Framework;
using Shouldly;

namespace GitVersionCore.Tests.Models
{
    public class CachedEnumerator_
    {
        private Random _random = new Random();

        // This just demonstrates the fact that an enumeration enumerated twice actually performs the enumeration twice.
        [Test]
        public void Enumerating_Underlying_Twice_Actually_Enumerates_Twice()
        {
            var items = Enumerable.Range(1, 100).Select(i => _random.Next());

            var enumerated1 = new List<int>();
            var enumerated2 = new List<int>();

            var enumerator1 = items.GetEnumerator();
            var enumerator2 = items.GetEnumerator();

            while (enumerator1.MoveNext())
            {
                enumerated1.Add(enumerator1.Current);
            }

            while (enumerator2.MoveNext())
            {
                enumerated2.Add(enumerator2.Current);
            }

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

            var enumerated1 = new List<int>();
            var enumerated2 = new List<int>();

            var enumerator1 = items.GetEnumerator().Cached("MØ");
            var enumerator2 = items.GetEnumerator();

            while (enumerator1.MoveNext())
            {
                enumerated1.Add(enumerator1.Current);
            }

            while (enumerator2.MoveNext())
            {
                enumerated2.Add(enumerator2.Current);
            }

            enumerated1.ShouldBeEquivalentTo(enumerated2);
        }


        [Test]
        public void Enumerates_Underlying_Only_Once_If_Enumerated_Twice()
        {
            // Here we don't enumerate to a list, so if it were no cache, the randoms should be evaluated for each
            // enumeration. Since the collections are equal, the results of the first enumeration are cached.
            var items = Enumerable.Range(1, 100).Select(i => _random.Next());

            var enumerated1 = new List<int>();
            var enumerated2 = new List<int>();

            var uniqueId = Guid.NewGuid().ToString();

            var enumerator = items.GetEnumerator().Cached(uniqueId);

            while (enumerator.MoveNext())
            {
                enumerated1.Add(enumerator.Current);
            }

            var enumerator2 = items.GetEnumerator().Cached(uniqueId);

            while (enumerator2.MoveNext())
            {
                enumerated2.Add(enumerator2.Current);
            }

            enumerated1.ShouldBeEquivalentTo(enumerated2);
        }
    }
}
