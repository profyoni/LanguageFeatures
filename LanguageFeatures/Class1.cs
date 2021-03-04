using System;
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

public class Foo
{
    public void Bar() { }

    // enumerator uses lazy evaluation = Just In Time
    public static IEnumerable<int> Range1(int min, int max) // Iterable
    {
        for (int x = min; x < max; x = x+1)
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
}

public interface IFoo
{
    double method(double elt);
}