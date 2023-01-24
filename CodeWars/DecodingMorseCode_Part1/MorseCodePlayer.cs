using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWars.DecodingMorseCode_Part1
{
    public static class MorseCodePlayer
    {
        const int timeUnitMS = 50;
        const int frequency = 800;

        public static void Play(string morseCode)
        {
            bool lastWasSpace = false;
            for (int i = 0; i < morseCode.Length; i++)
            {
                int interval = timeUnitMS * 3;
                if (morseCode[i] == '.')
                {
                    interval = timeUnitMS * 2;
                    Console.Beep(frequency, interval);
                }
                else if (morseCode[i] == '-')
                {
                    interval = timeUnitMS * 4;
                    Console.Beep(frequency, interval);
                }
                else if (morseCode[i] == ' ')
                {
                    interval = timeUnitMS * 2;
                    lastWasSpace = true;
                }

                Thread.Sleep(interval);
            }
        }

    }
}
