using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWars.NumberOfTrailingZerosOfN
{
    public static class Kata
    {
        public static int TrailingZeros(int n)
        {
            int kMax = (int)Math.Floor(Math.Log(n, 5));
            int zeros = 0;

            for (int k = 1; k <= kMax; k++)
            {
                var term = (int)Math.Floor(n / Math.Pow(5, k));
                zeros += term;
                if (0 == term)
                    break;
            }

            return zeros;
        }
    }
}
