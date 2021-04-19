using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LanguageFeatures.Test
{
    [TestClass]
    public class Linq
    {
        private IEnumerable<Person> all = Person.MakePeople();
        class PersonInitials
        {
            public String FirstInitial { get; set; }
            public String LastInitial { get; set; }
        }

        [TestMethod]
        public void Where()
        {

            var eiPeople =
                all.Where(p => p.FirstName.StartsWith("E") ||
                               p.FirstName.StartsWith("I"));

            eiPeople.Count().Should().Be(2);
        }
        [TestMethod]
        public void Select()
        {
            var testPeople =
                all.Select(p => new PersonInitials()
                {
                    FirstInitial = p.FirstName.Substring(0, 1),
                    LastInitial = p.LastName.Substring(0, 1),
                });

            testPeople.Count().Should().Be(26);
            testPeople.First().FirstInitial.Should().Be("A");
            testPeople.First().LastInitial.Should().Be("A");
            testPeople.Last().FirstInitial.Should().Be("Z");
            testPeople.Last().LastInitial.Should().Be("Z");
        }


        [TestMethod]
        public void Select2()
        {
            var testPeople =
                all.Select(p => new 
                {
                    FirstI = p.FirstName.Substring(0, 1),
                    LastI = p.LastName.Substring(0, 1),
                });

            testPeople.Count().Should().Be(26);
            testPeople.First().FirstI.Should().Be("A");
            testPeople.First().LastI.Should().Be("A");
            testPeople.Last().FirstI.Should().Be("Z");
            testPeople.Last().LastI.Should().Be("Z");
        }

        // C# In Depth
        // Effective C#
        [TestMethod]
        public void Single() // First and Where, fault tolerant, efficenicy, failsafe, def value for classes is null, 
        {
            var guy = all.FirstOrDefault(p => p.FirstName.StartsWith("!"));

            if (guy == null)
            {

            }
            guy.Should().Be(null);
//            guy.FirstName[0].Should().Be('B');
        }

        [TestMethod]
        public void SingleStruct() // default struct 
        {
            var n = new int[]{3,6,8,4}
                .FirstOrDefault(n => n == 9999);
            n.Should().Be(0);
        }

        // MoreLinq
        [TestMethod]
        public void GroupBy()
        {
            var n = Enumerable.Range(0,1000)
                .GroupBy(n => n % 5);

            n.Count().Should().Be(5);

            var grp1 = n.First();
            var grp2 = n.Last();

            grp1.Count().Should().Be(200);
            grp2.Count().Should().Be(200);

            grp1.Key.Should().Be(0);
            grp2.Key.Should().Be(4);
        }

        [TestMethod]
        public void Sum()
        {
            int sum = Enumerable.Range(0, 101).Sum();
            sum.Should().Be(5050);

            sum = Enumerable.Range(0, 101)
                .Aggregate(0, (s, n) => s + n);
            sum.Should().Be(5050);

            int prod = Enumerable.Range(0, 101)
                .Aggregate(1, (p, n) => p * n);

            prod = 1; // seed
            for (int n = 0; n < 1010; n++)
            {
                prod = prod * n;
            }
            prod.Should().Be(0);

            var r = new Random(-23908476);
            int next = r.Next(100);

            next.Should().Be(81);

            string[] fruits = { "banana","apple", "mango", "orange", "passionfruit", "grape" };

            // Determine largest string in array
            string longestName =
                fruits.Aggregate("",
                    (longest, next) =>
                        next.Length > longest.Length ? next : longest,
                    // Return the final result as an upper case string.
                    fruit => fruit.ToUpper());
            longestName.Should().Be("passionfruit".ToUpper());

            // Determine whether any string in the array is longer than "banana".
            fruits.Any(w => w.Length >= "banana".Length).Should().BeTrue();
            fruits.All(w => w.Length >= "banana".Length).Should().BeFalse();


            int x = 0;

            if (2 > 1 || x++ < 100)
                Console.WriteLine();

            x.Should().Be(0);

            if (2 < 1 && x++ < 100)
                Console.WriteLine();

            x.Should().Be(0);

            x = 37 % 9;
            x.Should().Be(1);

            x = -37 % 9; // remainder operator
            x.Should().Be(-1);
        }

        // shared by multi threads safely, no deep copy
        public class Fraction
        {
            private readonly int n,d;

            public int N
            {
                get { return n; }
            }
            public int D
            {
                get { return d; }
            }

            public Fraction(int n = 0, int d = 1)
            {
                this.n = n;
                this.d = d;
            }
        }
    }
}