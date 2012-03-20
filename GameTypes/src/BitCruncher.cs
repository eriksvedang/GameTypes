using System;
using System.Collections.Generic;
using System.Text;

namespace GameTypes
{
    public class BitCruncherException : Exception
    {
        public BitCruncherException(string pMessage) : base(pMessage) { }

    }
    public static class BitCruncher
    {
        public static int PackTwoShorts(int pA, int pB)
        { 
#if DEBUG
            if (pA > short.MaxValue || pA < short.MinValue || pB > short.MaxValue || pB < short.MinValue)
                throw new BitCruncherException("parameters must fit inside 16bit x:" + pA + " y:" + pB);
#endif
            int a = (ushort)pA;
            int b = (ushort)pB;
            return a | (b << 16); 
        }
        //switching between ushort to int is needed to keep the sign
        public static void UnpackTwoShorts(int packed, out int aResult, out int bResult)
        {
            aResult = (short)(packed & 0xffff);
            bResult = (short)((packed >> 16) & 0xffff);
        }

        public static long PackTwoInts(int pA, int pB)
        {
            long a = (uint)pA;
            long b = (uint)pB;
            return a | (b << 32); 
        }
        public static void UnpackTwoInts(long packed, out int aResult, out int bResult)
        {
            aResult = (int)(packed & 0xffffffff);
            bResult = (int)((packed >> 32) & 0xffffffff);
        }
        public static long PackFourShorts(int pA, int pB, int pC, int pD)
        {
            return PackTwoInts(PackTwoShorts(pA, pB), PackTwoShorts(pC, pD));
        }
        public static void UnpackFourShorts(long pSource, out int pA, out int pB, out int pC, out int pD)
        {
            int resultA, resultB;
            UnpackTwoInts(pSource, out resultA, out resultB);
            UnpackTwoShorts(resultA, out pA, out pB);
            UnpackTwoShorts(resultB, out pC, out pD);
        }
    }
}
