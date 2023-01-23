using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWars.DuplicateEncoder
{
    public class Kata
    {
        public static string DuplicateEncode(string word)
        {
            int[] charCount = new int[256];
            int capitalDelta = 'a' - 'A';

            for (int i = 0; i < word.Length; i++)
            {
                int charVal = (char)word[i];
                charCount[charVal]++;
            }

            for (int i = 0; i < 256; i++)
            {
                Console.WriteLine($"{i} - {(char)i}");
            }

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < word.Length; i++)
            {
                int charVal = (char)word[i];
                if (charCount[charVal] > 1)
                {
                    sb.Append(')');
                }
                else
                {
                    sb.Append('(');
                }
            }

            return sb.ToString();
        }


    }
}
