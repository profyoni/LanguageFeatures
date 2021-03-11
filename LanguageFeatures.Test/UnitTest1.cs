using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LanguageFeatures.Test
{
    [TestClass]
    public class UnitTest1
    {
        class Mystery : IFoo
        {
            public double method(double d)
            {
                return Math.Sqrt(d);
            }
        }
        [TestMethod]
        public void TestMethod1()
        {
            double actual = Class1.SumAfterComplexMethodCall(
                new List<double>() {9, 4, 1.0}, 
                new Mystery());
            Assert.AreEqual(6, actual);
        }

        [TestMethod]
        public void TestMethod2()
        {
            double actual = Class1.AggAfterComplexMethodCall2(
                new List<double> { 9, 4, 1.0 }, Math.Sqrt);
            Assert.AreEqual(6, actual);
        }

        [TestMethod]
        public void TestMethod3()
        {
            double actual = Class1.AggAfterComplexMethodCall2(
                new List<double> { 9, 4, 1.0 }, Math.Abs); // Foo(), the parentheses are called method invocation operator
            Assert.AreEqual(14, actual);
        }

        [TestMethod]
        public void TestMethod4()
        {
            double actual = Class1.AggAfterComplexMethodCall2(
                new List<double> { 9, 4, 1.0 }, Math.Abs); // Foo(), the parentheses are called method invocation operator
            Assert.AreEqual(14, actual);
        }
        // an event behaves like a delegate but less +=, -=, encapsulated delegate variable
        [TestMethod]
        public void MyAction() // multicast
        {
            Class1.MyAction act;

            act = Console.WriteLine;// act is a DELEGATE VARIABLE
            act += Console.Write;
            act += Console.WriteLine;
            act += Console.Write;
            act += Console.WriteLine;
            act += Console.WriteLine;

            act -= Console.Write;
            //act = null;

            act("WOO");
        }


        [TestMethod]
        public void NullProp() 
        {
            Foo f = null;
            if (new Random().Next() == 1)
                f = new Foo();

            f?.Bar();
        }

        [TestMethod]
        public void Yield() // yield allows for simplified IEnumerable
        {
            int sum = 0;
            foreach (int x in Foo.Range1(1,101))
            {
                sum += x;
            }
            Assert.AreEqual(5050, sum);
        }

        [TestMethod]
        public void FirstElt()
        {
            var enumerator = Foo.Range1(1, 100_000_001).GetEnumerator();
                enumerator.MoveNext();
            int x = enumerator.Current;
            Assert.AreEqual(1, x);

            enumerator = Foo.Range3(1, 100_000_001).GetEnumerator();
            enumerator.MoveNext();
            x = enumerator.Current;
            Assert.AreEqual(1, x);

            enumerator = Foo.Range2(1, 100_000_001).GetEnumerator();
            enumerator.MoveNext();
            x = enumerator.Current;
            Assert.AreEqual(1, x);
        }

        [TestMethod]
        public void Linq1()
        {
            var multiplesOf3 =
                Foo.Range1(1, 101).Where(Div3);
            Assert.AreEqual(33, multiplesOf3.Count());
        }

        [TestMethod]
        public void Linq2()
        {
            var multiplesOf3 =
                Foo.Range2(1, 101).Where(Div3);
            Assert.AreEqual(33, multiplesOf3.Count());
        }

        [TestMethod]
        public void Linq3()
        {
            var multiplesOf3 =
                Foo.Range3(1, 101).Where(Div3);
            Assert.AreEqual(33, multiplesOf3.Count());
        }

        private bool Div3(int arg)
        {
            return arg % 3 == 0;
        }

        // Deferred Execution  -delay execution until the value is needed - optimization avoid computation if not actually needed
        // Method Extension Syntax
        // Query Syntax
        [TestMethod]
        public void Linq5()
        {
            Func<int, bool> myfunction = Over80;

            // Specify the data source.
            int[] scores = new int[] { 97, 43, 23, 65, 67, 97, 92, 81, 60 };

            // Define the query expression.
            IEnumerable<int> scoreQuery =
                from score in scores
                where score > 80
                select score;

            var firstOver80 = scores.Where(myfunction).First();

            Assert.AreEqual(97, firstOver80);
        }
           
        static int count;
        private bool Over80(int g)
        {
            Console.WriteLine(count++);
            return g > 80;
        }
    }

    
}


[TestClass]
public class UnitTest2
{
    // Where, Take, TakeWhile,
    // Sum, Min, Max, Count
    // Projection = Computed Column/Field

    [TestMethod]
    public void Primes()
    {
        Func<int, bool> func = x => x < 10;

        int sum = Homework1.Primes()
            .TakeWhile(x => x < 10)
            .Sum();

        Assert.AreEqual(2+3+5+7, sum);
    }

    [TestMethod]
    public void Primes2()
    {
        int sum = Homework1.Primes()
            .SkipWhile(x => x < 100)
            .Take(2)
            .Sum();

        Assert.AreEqual(101 + 103, sum);
    }
    [TestMethod]
    public void Projection1()
    {
        int sum = Homework1.Primes()  // 2 3 5 7 11 ...
            .Select(x => x + 1)   // 3 4 6 8 12
            .Take(2)
            .Sum();

        Assert.AreEqual(7, sum);
    }

    [TestMethod]
    public void Projection2()
    {
        var sum = Enumerable.Range(1, 100)
            .Select(x => Math.Pow(x, 2))
            .Take(2)
            .Sum();

        Assert.AreEqual(5, sum, 0.00001);
    }

    class Person
    {
        public int ID { get; set; }
        public String Name { get; set; }
    }
    [TestMethod]
    public void Projection3()
    {
        var people = Enumerable.Range(1, 100)
            .Select(x => new Person {ID = x, Name = GetNameFromDB(x)})
            .ToList();

        Assert.AreEqual(true, people.Count == 100);
    }

    private string GetNameFromDB(int i)
    {
        return "Harold" + i;
    }

    private bool LessThan10(int arg)
    {
        return arg < 10;
    }

    [TestMethod]
    public void IsPrime()
    {
        Assert.AreEqual(true, 19.IsPrime());
        Assert.AreEqual(false, 100010001.IsPrime());
    }
}