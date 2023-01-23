using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWars.HelloWorld
{
    public static class Kata
    {
        public static string Greet()
        {
            StringBuilder hello = new();
            StringBuilder world = new();
            for (int i = 0; i < 5; i++)
            {
                //"hello" and "world" are found in the noise.
                //Couldn't find more than 5 characters in the correct order, the chances are slim.
                hello.Append(NoisyChar(47164155 + i));
                world.Append(NoisyChar(25961485 + i));
            }

            return $"{hello} {world}!";
        }

        static char NoisyChar(int seed)
        {
            var val = (Math.Sin(seed * 874399.0) / 2.0 + 0.5) * 234781.0;
            var fract = val - Math.Truncate(val);
            char charVal = (char)(fract * 91.0 + ' ');

            return charVal;
        }

        //static int FindAnagramInNoise(string s)
        //{

        //}

        static int FindStringInNoise(string s)
        {
            int position = -1;
            for (int i = 0; i < int.MaxValue; i++)
            {
                bool hwFound = false;
                for (int c = 0; c < s.Length; c++)
                {

                    if (NoisyChar(i + c) != s[c])
                    {
                        //Is not a full match
                        int curIndex = i;
                        string foundSubStr = new string(s.Take(c + 1).ToArray());

                        if (c > 3)
                            Console.WriteLine($"partial found at {curIndex}: {foundSubStr}");

                        i += c;
                        break;
                    }


                    if (c == s.Length - 1)
                    {
                        Console.WriteLine(i);
                        hwFound = true;
                        position = i;
                        break;
                    }
                }

                if (hwFound)
                    break;
            }

            return position;
        }

    }
}
