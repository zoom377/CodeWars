using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using CodeWars.DecodingMorseCode_Part1;
using Dia2Lib;
using Perfolizer.Mathematics.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeWars
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var rudeMorseCode = MorseCodeGenerator.Generate("BUM SMELL!");
            Console.WriteLine(rudeMorseCode);
            MorseCodePlayer.Play(rudeMorseCode);

            var testParams = new string[]
            {
                MorseCodeGenerator.Generate("a"),
                MorseCodeGenerator.Generate("b"),
                MorseCodeGenerator.Generate("c"),
                MorseCodeGenerator.Generate("d"),
                MorseCodeGenerator.Generate("e"),
                MorseCodeGenerator.Generate("f"),
                MorseCodeGenerator.Generate("g"),
                MorseCodeGenerator.Generate("h"),
                MorseCodeGenerator.Generate("i"),
                MorseCodeGenerator.Generate("j"),
                MorseCodeGenerator.Generate("k"),
                MorseCodeGenerator.Generate("l"),
                MorseCodeGenerator.Generate("m"),
                MorseCodeGenerator.Generate("n"),
                MorseCodeGenerator.Generate("o"),
                MorseCodeGenerator.Generate("p"),
                MorseCodeGenerator.Generate("q"),
                MorseCodeGenerator.Generate("r"),
                MorseCodeGenerator.Generate("s"),
                MorseCodeGenerator.Generate("t"),
                MorseCodeGenerator.Generate("u"),
                MorseCodeGenerator.Generate("v"),
                MorseCodeGenerator.Generate("w"),
                MorseCodeGenerator.Generate("x"),
                MorseCodeGenerator.Generate("y"),
                MorseCodeGenerator.Generate("z"),
                MorseCodeGenerator.Generate("."),
                MorseCodeGenerator.Generate("!"),
                //MorseCodeGenerator.Generate("SOS"),
                MorseCodeGenerator.Generate("I LIKE TO PLAY GUITAR"),
                MorseCodeGenerator.Generate(" WAY   TOO      MUCH CODING    "),
                "... --- ...",
                "...---...",
            };

            //Console.WriteLine(MorseCodeDecoder.GenerateMorseMap());

            for (int i = 0; i < testParams.Length; i++)
            {
                var val = MorseCodeDecoder.Decode(testParams[i]);
                Console.WriteLine(val);
            }

            Console.Write("Press any to start benchmark");
            Console.Read();
            IConfig con = DefaultConfig.Instance.WithOptions(ConfigOptions.DisableOptimizationsValidator);
            BenchmarkRunner.Run<Benchmarks>(con);

        }
    }
}