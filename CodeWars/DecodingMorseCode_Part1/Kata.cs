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



        const string morseMapAlt = $"0ET\0IA\0NM\0\0\0\0SU\0RW\0\0\0\0DK\0GO\0\0\0\0\0\0\0\0\0\0\0\0\0HV\0F\0\0\0\0\0L\0\0PJ\0\0\0\0\0\0\0\0\0\0\0\0\0BX\0CY\0\0\0\0ZQ\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\054\0\03\0\0\0\0\0\0\0\02\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0!\0\0\0\0\0\0\0\0\0\01\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\06\0\0\0\0\0\0\0\0\0\0.\0\0\0\0\0\0\0\0\0\0\0\0\0\0\07\0\0\0\0\0\0\0\08\0\09";

        //Dictionary can't be const so we use string instead as a map.
        //About 1/3rd of the memory of a dictionary, 1/10th of the run time (according to benchmarkdotnet)
        const string oldMorseMap = $"\0ET\0IA\0NM\0\0\0\0SU\0RW\0\0\0\0DK\0GO\0\0\0\0\0\0\0\0\0\0\0\0\0HV\0F\0\0\0\0\0L\0\0PJ\0\0\0\0\0\0\0\0\0\0\0\0\0BX\0CY\0\0\0\0ZQ\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\054\0\03\0\0\0\0\0\0\0\02\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\01\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\06\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\07\0\0\0\0\0\0\0\08\0\090";
        public static string DecodeOld(string morseCode)
        {
            int start = 0, end = morseCode.Length - 1;
            while (start < end && morseCode[start] == ' ')
                start++;
            while (end > start && morseCode[end] == ' ')
                end--;

            StringBuilder sb = new();
            int base3Val = 0;
            for (int i = start; i <= end; i++)
            {
                if (morseCode[i] == '.')
                {
                    base3Val *= 3;//Move to next digit of base3 number. If currentChar is 0, this has no effect.
                    base3Val += 1;
                }
                else if (morseCode[i] == '-')
                {
                    base3Val *= 3;
                    base3Val += 2;
                }
                else //morseCode[i] == ' '
                {
                    //End of morse character sequence reached
                    if (base3Val < 243)
                        sb.Append(oldMorseMap[base3Val]);
                    else if (base3Val == 455)
                        sb.Append(".");
                    else if (base3Val == 634)
                        sb.Append("!");
                    else if (base3Val == 10192)
                        sb.Append("SOS");

                    base3Val = 0;
                    if (i <= end - 3 && morseCode[i + 1] == ' ')
                    {
                        i += 2;
                        sb.Append(' ');
                    }

                }
            }

            //Add final char
            if (base3Val < 243)
                sb.Append(oldMorseMap[base3Val]);
            else if (base3Val == 455)
                sb.Append(".");
            else if (base3Val == 634)
                sb.Append("!");
            else if (base3Val == 10192)
                sb.Append("SOS");

            return sb.ToString();
        }

        //const string morseMap = "\0\0ETINAMSDRGUKWOHBLZFCP#VX#Q#YJ#56#7###8#######9#4######3###2#10##########################################.##########!";
        const string morseMap = "\0\0ETIANMSURWDKGOHVF#L#PJBXCYZQ##54#3###2#######16#######7###8#90#####################.#####################!";
        public static string Decode(string morseCode)
        {
            int start = 0, end = morseCode.Length - 1;
            while (start < end && morseCode[start] == ' ')
                start++;
            while (end > start && morseCode[end] == ' ')
                end--;

            StringBuilder sb = new(morseCode.Length / 3 + 1);
            int charVal = 1;
            for (int i = start; i <= end; i++)
            {
                if (morseCode[i] == '.')
                {
                    charVal <<= 1;
                }
                else if (morseCode[i] == '-')
                {
                    charVal <<= 1;
                    charVal++;
                }
                else //morseCode[i] == ' '
                {
                    if (charVal >= 256)
                        sb.Append("SOS");
                    else
                        sb.Append(morseMap[charVal]);

                    charVal = 1;
                    for (int j = i+1; j <= end; j++)
                    {
                        if ((j - i + 1) % 3 == 0)
                            sb.Append(' ');

                        if (morseCode[j] != ' ')
                        {
                            i = j - 1;
                            break;
                        }

                    }
                }
            }

            if (charVal >= 256)
                sb.Append("SOS");
            else
                sb.Append(morseMap[charVal]);

            return sb.ToString();
        }

        public static string GenerateMorseMap()
        {
            Dictionary<int, char> map = new()
            {
                { 2,   'E' },
                { 3,   'T' },
                { 4,   'I' },
                { 5,   'A' },
                { 6,   'N' },
                { 7,   'M' },
                { 8,   'S' },
                { 9,   'U' },
                { 10,  'R' },
                { 11,  'W' },
                { 12,  'D' },
                { 13,  'K' },
                { 14,  'G' },
                { 15,  'O' },
                { 16,  'H' },
                { 17,  'V' },
                { 18,  'F' },
                { 20,  'L' },
                { 22,  'P' },
                { 23,  'J' },
                { 24,  'B' },
                { 25,  'X' },
                { 26,  'C' },
                { 27,  'Y' },
                { 28,  'Z' },
                { 29,  'Q' },
                { 32,  '5' },
                { 33,  '4' },
                { 35,  '3' },
                { 39,  '2' },
                { 47,  '1' },
                { 48,  '6' },
                { 56,  '7' },
                { 60,  '8' },
                { 62,  '9' },
                { 63,  '0' },
                { 85,  '.' },
                { 107, '!' }
            };

            StringBuilder sb = new();
            for (int i = 0; i < 256; i++)
            {
                if (map.ContainsKey(i))
                {
                    sb.Append(map[i]);
                }
                else
                {
                    sb.Append('#');
                }
            }

            return sb.ToString();
        }
    }
}
