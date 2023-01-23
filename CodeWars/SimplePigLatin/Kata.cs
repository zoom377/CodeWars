using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWars.SimplePigLatin
{
    public class Kata
    {
        public static string PigIt(string str)
        {
            //Tokenise string
            List<string> words = new();
            int wordStart = -1;
            int wordEnd = -1;

            bool word = false;
            for (int i = 0; i < str.Length; i++)
            {
                if (!char.IsLetter(str[i]))
                {
                    words.Add($"{str.Substring(wordStart+1, i-wordStart)}{str[wordStart]}ay");
                }
            }

            return str;
        }
    }
}
