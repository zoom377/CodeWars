using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using CodeWars.DecodingMorseCode_Part1;
using Perfolizer.Mathematics.Common;
using System;

namespace CodeWars
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var testParam = new string[]
            {
                ".",
                "-",
                "..",
                ".-",
                "-.",
                "--",
                "...",
                "..-",
                ".-.",
                ".--",
                "-..",
                "-.-",
                "--.",
                "---",
                "....",
                "...-",
                "..-.",
                ".-..",
                ".--.",
                ".---",
                "-..-",
                "-.-.",
                "-.--",
                "-...",
                "--..",
                "--.-",
                ".----",
                "..---",
                "...--",
                "....-",
                ".....",
                "-....",
                "--...",
                "---..",
                "----.",
                "-----"
            };

            foreach (var param in testParam)
            {
                Console.WriteLine(DecodingMorseCode_Part1.MorseCodeDecoder.Decode(param));
            }

            Console.WriteLine();

            var res = DecodingMorseCode_Part1.MorseCodeDecoder.Decode(".... . -.--   .--- ..- -.. .");
            Console.WriteLine(res);

            IConfig con = ConfigExtensions.WithOptions(DefaultConfig.Instance, ConfigOptions.DisableOptimizationsValidator);
            BenchmarkRunner.Run<Benchmarks>(con);

        }
    }
}