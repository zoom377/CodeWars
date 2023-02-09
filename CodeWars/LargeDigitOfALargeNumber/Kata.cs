using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWars.LargeDigitOfALargeNumber
{
    using System.Linq.Expressions;
    using System.Numerics;

    class LastDigit
    {
        public static int GetLastDigit(BigInteger n1, BigInteger n2)
        {
            if (n2 == 0)
                return 1;

            //Each last digit that a number could have (0-9) will cycle through a pattern
            //  as it is multiplied, regardless of the more significant digits.
            //0:    0
            //1:    1,2,3,4,5,6,7,8,9,0
            //2:    2,4,6,8,0
            //3:    3,9,7,1
            //4:    4,6
            //5:    5
            //6:    6
            //7:    7,9,3,1
            //8:    8,4,2,6
            //9:    9,1
            byte[][] patterns = new byte[][]
            {
                new byte[]{0},
                new byte[]{1,2,3,4,5,6,7,8,9,0},
                new byte[]{2,4,6,8,0},
                new byte[]{3,9,7,1},
                new byte[]{4,6},
                new byte[]{5},
                new byte[]{6},
                new byte[]{7,9,3,1},
                new byte[]{8,4,2,6},
                new byte[]{9,1},
            };

            byte[] pattern = patterns[(int)n1 % 10];
            return (int)pattern[(int)n2 % pattern.Length];
        }
    }
}
