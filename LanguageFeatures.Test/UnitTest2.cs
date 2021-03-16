using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LanguageFeatures.Test
{
    [TestClass]
    public class UnitTest2
    {
        class Mystery2
        {
            public static void Method(ref string d)
            {
                d = d + d;
            }

            public static void Method2(double d)
            {
                d = d + d;
            }
            public static bool TryParseInt(string d, out int x)
            {
                x = 0;
                foreach (char c in d)
                {
                    if (c >= '0' && c <= '9')
                    {
                        x = x * 10 + (c - '0');
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;

            }
        }

        [TestMethod]
        public void PassByReference()
        {
            var x = "1";
            Mystery2.Method(ref x);
            Assert.AreEqual("11", x);

            decimal dd = 98;
           // not legal Mystery2.Method(ref dd);
        }

        [TestMethod]
        public void PassByValue()
        {
            var x = 1.4;
            Mystery2.Method2(x);
            Assert.AreEqual(1.4, x);


            Mystery2.Method2(2);
        }

        [TestMethod]
        public void TryParseInt()
        {
            int asInt = 9;
            var x = "13801";
            if (Mystery2.TryParseInt(x, out asInt))
                Assert.AreEqual(13801, asInt);

            if (Int32.TryParse(x, out var asInt2))
                Assert.AreEqual(13801, asInt2);
        }
    }
}