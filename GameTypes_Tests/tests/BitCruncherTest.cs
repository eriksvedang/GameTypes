using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace GameTypes.tests
{
    [TestFixture]
    public class BitCruncherTest
    {
        [Test]
        public void PackTwoPositiveNumbers()
        {
            int a = 1234;
            int b = 4567;
            int packedInt = BitCruncher.PackTwoShorts(a, b);

            int aResult;
            int bResult;
            BitCruncher.UnpackTwoShorts(packedInt, out aResult, out bResult);

            Assert.AreEqual(a, aResult);
            Assert.AreEqual(b, bResult);
        }
        [Test]
        public void PackTwoNegativeNumbers()
        {
            int a = -1234;
            int b = -4567;
            int packed = BitCruncher.PackTwoShorts(a, b);

            int aResult;
            int bResult;
            BitCruncher.UnpackTwoShorts(packed, out aResult, out bResult);

            Assert.AreEqual(a, aResult);
            Assert.AreEqual(b, bResult);
        }
        [Test]
        public void PackTwoMixedNumbers()
        {
            int a = 1234;
            int b = -4567;
            int packed = BitCruncher.PackTwoShorts(a, b);

            int aResult;
            int bResult;
            BitCruncher.UnpackTwoShorts(packed, out aResult, out bResult);

            Assert.AreEqual(a, aResult);
            Assert.AreEqual(b, bResult);
        }
        [Test]
        public void PackTwoUnderflowAndOverflow()
        {
            int a = int.MaxValue;
            int b = int.MinValue;
            int packed = 99;
            Assert.Throws<BitCruncherException>(
                () =>
                {
                    packed = BitCruncher.PackTwoShorts(a, 0);
                }
                );
            Assert.Throws<BitCruncherException>(
                () =>
                {
                    packed = BitCruncher.PackTwoShorts(0, b);
                }
                );
            Assert.AreEqual(99, packed);

        }
        [Test]
        public void PackTwoInts()
        {
            int aResult, bResult;
            int a = short.MaxValue + 4126;
            int b = short.MinValue - 9999;
            long packedLong = BitCruncher.PackTwoInts(a, b);
            BitCruncher.UnpackTwoInts(packedLong, out aResult, out bResult);
            Assert.AreEqual(a, aResult);
            Assert.AreEqual(b, bResult);
        }
        [Test]
        public void PackFour()
        { 
            int a = 10, b = -12, c = 24, d = 99;
            int ar, br, cr, dr;
            long packed = BitCruncher.PackFourShorts(a, b, c, d);
            BitCruncher.UnpackFourShorts(packed, out ar, out br, out cr, out dr);
            Assert.AreEqual(a, ar);
            Assert.AreEqual(b, br);
            Assert.AreEqual(c, cr);
            Assert.AreEqual(d, dr);

        }

        


    }
}

