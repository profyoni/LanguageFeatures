using System;
using System.Collections;
using System.Collections.Generic;

namespace LanguageFeatures
{
    public class Class1
    {
        // sum all numbers list after we performed some unknown yet Complex Operation


        public static double SumAfterComplexMethodCall(List<double> list, IFoo foo)
        {
            double sum = 0;
            foreach (var elt in list)
            {
                sum += foo.method(elt);
            }

            return sum;
        }

        public delegate double MathMethod(double d);// delegate is data type to represent a method
        public delegate double Aggregator(List<double> d);// delegate is data type to represent a method
        public delegate void MyAction(Object o);// delegate is DELEGATE DATA TYPE to represent a method

        public event MathMethod MyEvent;
        public static double Sum(List<double> list)
        {
            var sum = 0.0;
            foreach (var d in list)
            {
                sum += d;
            }

            return sum;
        }

        public static double AggAfterComplexMethodCall2(
            List<double> list, 
            MathMethod method, 
            Aggregator agg = null)
        {
            List<double> ret = new List<double>();
            foreach (var elt in list)
            {
                ret.Add(method(elt));
            }

            return (agg ?? Sum)(ret); // replaces if (agg==null) agg=Sum;
        }

        // LINQ - Language Integrated Queries - 

    }


}

public class Person
{
    public String FirstName { get; set; }
    public String LastName { get; set; }

    public static IEnumerable<Person> MakePeople()
    {
        for (int i = 0; i < 26; i++)
        {
            yield return new Person
            {
                FirstName = "" + (char)(i+'A') + "rishon",
                LastName = "" + (char)(i + 'A') + "qua"
            };
        }
    }
}


public class Foo
{
    public void Bar() { }

    // enumerator uses lazy evaluation = Just In Time
    public static IEnumerable<int> Range1(int min, int max) // Iterable
    {
        for (int x = min; x < max; x++)
            yield return x; // stores the current stack record to return to
    }

    // enumerator uses eager evaluation
    public static IEnumerable<int> Range2(int min, int max) // Iterable
    {
        List<int> list = new List<int>();
        for (int x = min; x < max; x++)
            list.Add(x);
        return list;
    }

    //Using standard Enumerator
    public static IEnumerable<int> Range3(int min, int max) // Iterable
    {
        return new MyEnumerable(min, max);
    }
}

public class MyEnumerable : IEnumerable<int>
{
    private readonly int _min;
    private readonly int _max;

    public MyEnumerable(int min, int max)
    {
        _min = min;
        _max = max;
    }

    public IEnumerator<int> GetEnumerator()
    {
        return new MyEnumerator(_min, _max);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class MyEnumerator : IEnumerator<int>
{
    private readonly int _min;
    private readonly int _max;
    public MyEnumerator(int min, int max)
    {
        _min = min;
        _max = max;
    }

    public bool MoveNext()
    {
        if (Current >= _max)
            return false;
        Current++;
        return true;
    }

    public void Reset()
    {
        Current = _min;
    }

    public int Current { get; private set; }

    object IEnumerator.Current => Current;

    public void Dispose()
    { }
}

public interface IFoo
{
    double method(double elt);
}

public static class Homework1 // all methods must be static - utility class, uninstantiable
{
    public static IEnumerable<int> Primes()
    {
        for (int i = 2; i < Int32.MaxValue; i++)
        {
            if (IsPrime(i))
                yield return i;
        }
    }

    public static bool IsPrime(this int n)
    {
        if (n == 2)
            return true;
        if (n == 1)
            return false;
        for (int i = 2; i <= Math.Sqrt(n);i++)
        {
            if (n % i == 0)
                return false;
        }
        return true;

    }
}