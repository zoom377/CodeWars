using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWars.DecodingMorseCode_Part1
{
    class MorseCodeDecoder
    {

        //Maybe morse code can be represented with base 3 numbers.
        //English letters can be represented with up to 4 morse code digits.

        //Numbers can be represented with 5 morse digits.
        //Space ' ' = 0
        //Dot   '.' = 1
        //Dash  '-' = 2

        //Parsing psuedocode:
        //
        //int letter = 0;
        //For each char:
        //  If (char is not a space)
        //  {
        //      letter *= 10;
        //
        //      if (char is '.')
        //          letter += 1;
        //
        //      if (char is '_')
        //          letter += 2;
        //  }

        //Base 3:
        //00001 = 1
        //00002 = 2
        //00010 = 3
        //00011 = 4
        //00012 = 5
        //00020 = 6
        //00100 = 9
        //01000 = 27
        //10000 = 81

        //Converted to Base3 (right padded with zeros)
        //Code  Char    Morse   Base3   Decimal
        //69    E       .       10000   81
        //73    I       ..      11000   108
        //83    S       ...     11100   117
        //72    H       ....    11110   120
        //86    V       ...-    11120   123
        //85    U       ..-     11200   126
        //70    F       ..-.    11210   129
        //65    A       .-      12000   135
        //82    R       .-.     12100   144
        //76    L       .-..    12110   147
        //87    W       .--     12200   153
        //80    P       .--.    12210   156
        //74    J       .---    12220   159
        //84    T       -       20000   162
        //78    N       -.      21000   189
        //68    D       -..     21100   198
        //66    B       -...    21110   201
        //88    X       -..-    21120   204
        //75    K       -.-     21200   207
        //67    C       -.-.    21210   210
        //89    Y       -.--    21220   213
        //77    M       --      22000   216
        //71    G       --.     22100   225
        //90    Z       --..    22110   228
        //81    Q       --.-    22120   231
        //79    O       ---     22200   246
        //48    1       .----   12222   161
        //49    2       ..---   11222   134
        //50    3       ...--   11122   125
        //51    4       ....-   11112   122
        //52    5       .....   11111   121
        //53    6       -....   21111   202
        //54    7       --...   22111   229
        //55    8       ---..   22211   238
        //56    9       ----.   22221   241
        //57    0       -----   22222   242


        //Converted to Base3 (left padded with zeros)
        //Code  Char    Morse   Base3   Decimal
        //69    E       .       00001   1 
        //84    T       -       00002   2 
        //73    I       ..      00011   4 
        //65    A       .-      00012   5 
        //78    N       -.      00021   7 
        //77    M       --      00022   8 
        //83    S       ...     00111   13
        //85    U       ..-     00112   14
        //82    R       .-.     00121   16
        //87    W       .--     00122   17
        //68    D       -..     00211   22
        //75    K       -.-     00212   23
        //71    G       --.     00221   25
        //79    O       ---     00222   26
        //72    H       ....    01111   40
        //86    V       ...-    01112   41
        //70    F       ..-.    01121   43
        //76    L       .-..    01211   49
        //80    P       .--.    01221   52
        //74    J       .---    01222   53
        //66    B       -...    02111   67
        //88    X       -..-    02112   68
        //67    C       -.-.    02121   70
        //89    Y       -.--    02122   71
        //90    Z       --..    02211   76
        //81    Q       --.-    02212   77
        //48    1       .----   12222   161
        //49    2       ..---   11222   134
        //50    3       ...--   11122   125
        //51    4       ....-   11112   122
        //52    5       .....   11111   121
        //53    6       -....   21111   202
        //54    7       --...   22111   229
        //55    8       ---..   22211   238
        //56    9       ----.   22221   241
        //57    0       -----   22222   242


        const string morseMap = " ET IA NM    SU RW    DK GO             HV F     L  PJ             BX CY    ZQ                                           54  3        2                          1                                        6                          7        8  90";


        public static string Decode(string morseCode)
        {
            StringBuilder sb = new();
            int base3Val = 0;
            for (int i = 0; i < morseCode.Length + 1; i++)
            {
                if (i >= morseCode.Length || morseCode[i] == ' ')
                {
                    //End of morse character sequence reached
                    sb.Append(morseMap[base3Val]);
                    base3Val = 0;
                    if (i < morseCode.Length && morseCode[i + 1] == ' ')
                    {
                        i += 2;
                        sb.Append(' ');
                    }

                    continue;
                }

                //Move to next digit of base3 number. If currentChar is 0, this has no effect.
                base3Val *= 3;

                if (morseCode[i] == '.')
                    base3Val += 1;

                if (morseCode[i] == '-')
                    base3Val += 2;
            }

            return sb.ToString();
        }

        public static string DecodeDict(string morseCode)
        {
            //243 - 36 = 207 wasted bytes
            //But maybe faster than dictionary
            //char[] morseMap = new char[243];
            Dictionary<int, char> morseMap = new()
            {
                {1, 'E'},
                {2, 'T'},
                {4, 'I'},
                {5, 'A'},
                {7, 'N'},
                {8, 'M'},
                {13, 'S'},
                {14, 'U'},
                {16, 'R'},
                {17, 'W'},
                {22, 'D'},
                {23, 'K'},
                {25, 'G'},
                {26, 'O'},
                {40, 'H'},
                {41, 'V'},
                {43, 'F'},
                {49, 'L'},
                {52, 'P'},
                {53, 'J'},
                {67, 'B'},
                {68, 'X'},
                {70, 'C'},
                {71, 'Y'},
                {76, 'Z'},
                {77, 'Q'},
                {121, '5'},
                {122, '4'},
                {125, '3'},
                {134, '2'},
                {161, '1'},
                {202, '6'},
                {229, '7'},
                {238, '8'},
                {241, '9'},
                {242, '0'},
        };

            //MorseCodeDecoder.Decode(".... . -.--   .--- ..- -.. .")
            //should return "HEY JUDE"

            StringBuilder sb = new();
            int base3Val = 0;
            for (int i = 0; i < morseCode.Length + 1; i++)
            {
                if (i >= morseCode.Length || morseCode[i] == ' ')
                {
                    //End of morse character sequence reached
                    sb.Append(morseMap[base3Val]);
                    base3Val = 0;
                    if (i < morseCode.Length && morseCode[i + 1] == ' ')
                    {
                        i += 2;
                        sb.Append(' ');
                    }

                    continue;
                }

                //Move to next digit of base3 number. If currentChar is 0, this has no effect.
                base3Val *= 3;

                if (morseCode[i] == '.')
                    base3Val += 1;

                if (morseCode[i] == '-')
                    base3Val += 2;
            }

            return sb.ToString();
        }
    }
}
