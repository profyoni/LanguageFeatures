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
        public void Linq1()
        {
            var multiplesOf3 = 
                Foo.Range1(1, 101).Where(Div3);
            Assert.AreEqual(33, multiplesOf3.Count());
        }

        private bool Div3(int arg)
        {
            return arg % 3 == 0;
        }
    }

    
}
