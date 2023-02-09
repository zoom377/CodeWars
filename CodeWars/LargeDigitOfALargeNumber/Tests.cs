using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWars.LargeDigitOfALargeNumber
{
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Numerics;

    // TODO: Replace examples and use TDD development by writing your own tests

    [TestFixture]
    public class SolutionTest
    {
        [Test]
        public void ExampleTests()
        {
            Assert.AreEqual(4, LastDigit.GetLastDigit(4, 1));
            Assert.AreEqual(6, LastDigit.GetLastDigit(4, 2));
            Assert.AreEqual(9, LastDigit.GetLastDigit(9, 7));
            Assert.AreEqual(0, LastDigit.GetLastDigit(10, BigInteger.Pow(10, 10)));
            Assert.AreEqual(6, LastDigit.GetLastDigit(BigInteger.Pow(2, 200), BigInteger.Pow(2, 300)));
            Assert.AreEqual(7, LastDigit.GetLastDigit(BigInteger.Parse("3715290469715693021198967285016729344580685479654510946723"), BigInteger.Parse("68819615221552997273737174557165657483427362207517952651")));
        }

        [Test]
        public void XPowZero()
        {
            foreach (var d in Enumerable.Range(0, 9))
            {
                Assert.AreEqual(1, LastDigit.GetLastDigit(d, 0));
            }
        }
    }
}
