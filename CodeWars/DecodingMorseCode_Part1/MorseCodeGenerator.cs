using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWars.DecodingMorseCode_Part1
{
    public class MorseCodeGenerator
    {

        public static string Generate(string english)
        {
            Dictionary<char, string> map = new Dictionary<char, string>()
            {
                {'E',"." },
                {'T',"-"},
                {'I',".."},
                {'A',".-"},
                {'N',"-."},
                {'M',"--"},
                {'S',"..."},
                {'U',"..-"},
                {'R',".-."},
                {'W',".--"},
                {'D',"-.."},
                {'K',"-.-"},
                {'G',"--."},
                {'O',"---"},
                {'H',"...."},
                {'V',"...-"},
                {'F',"..-."},
                {'L',".-.."},
                {'P',".--."},
                {'J',".---"},
                {'B',"-..."},
                {'X',"-..-"},
                {'C',"-.-."},
                {'Y',"-.--"},
                {'Z',"--.."},
                {'Q',"--.-"},
                {'1',".----"},
                {'2',"..---"},
                {'3',"...--"},
                {'4',"....-"},
                {'5',"....."},
                {'6',"-...."},
                {'7',"--..."},
                {'8',"---.."},
                {'9',"----."},
                {'0',"-----"},
                {'.',".-.-.-"},
                {'!',"-.-.--"},
                {'@',"SOS"},
                {' '," "},
            };

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < english.Length; i++)
            {
                char c = english[i];
                if (char.IsLetter(c))
                {
                    sb.Append(map[char.ToUpper(c)]);
                }
                else
                { 
                    sb.Append(map[c]);
                }
                if (i < english.Length - 1)
                    sb.Append(map[' ']);
            }

            return sb.ToString();
        }
    }
}
