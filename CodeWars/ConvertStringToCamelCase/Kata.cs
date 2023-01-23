using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

//Complete the method/function so that it converts dash/underscore delimited words into camel casing.
//The first word within the output should be capitalized only if the original word was capitalized (known as Upper Camel Case, also often referred to as Pascal case).
//The next words should be always capitalized.

//Examples
//"the-stealth-warrior" gets converted to "theStealthWarrior"
//"The_Stealth_Warrior" gets converted to "TheStealthWarrior"

namespace CodeWars.ConvertStringToCamelCase
{
    public class Kata
    {
        public static string ToCamelCase(string str)
        {
            StringBuilder sb = new();

            sb.Append(str[0]);
            for (int i = 1; i < str.Length; i++)
            {
                if (str[i] == '-' || str[i] == '_')
                {   
                    if (i+1 < str.Length)
                    {
                        sb.Append(char.ToUpper(str[i + 1]));
                        i++;
                    }
                    continue;
                }
                sb.Append(str[i]);
            }

            return sb.ToString(); ;
        }
    }
}
