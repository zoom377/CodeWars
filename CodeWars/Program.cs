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

            MorseCodeDecoder.Decode(".-.-");

            IConfig con = ConfigExtensions.WithOptions(DefaultConfig.Instance, ConfigOptions.DisableOptimizationsValidator);
            BenchmarkRunner.Run<Benchmarks>(con);

            //var testParam = new string[]
            //{
            //    ".",
            //    "-",
            //    "..",  
            //    ".-",
            //    "-.",
            //    "--",
            //    "...",  
            //    "..-",
            //    ".-.",
            //    ".--",
            //    "-..",
            //    "-.-",
            //    "--.",
            //    "---",
            //    "....",
            //    "...-",
            //    "..-.",
            //    ".-..", 
            //    ".--.",
            //    ".---",
            //    "-..-",
            //    "-.-.",
            //    "-.--",
            //    "-...", 
            //    "--..",
            //    "--.-",
            //    ".----",
            //    "..---",
            //    "...--",
            //    "....-",
            //    ".....",
            //    "-....",
            //    "--...",
            //    "---..",
            //    "----.",
            //    "-----"
            //};

            //foreach (var param in testParam) 
            //{
            //    Console.WriteLine(DecodingMorseCode_Part1.MorseCodeDecoder.Decode(param));
            //}

            //Console.WriteLine();

            //var res = DecodingMorseCode_Part1.MorseCodeDecoder.Decode(".... . -.--   .--- ..- -.. .");
            //Console.WriteLine(res);


            //HelloWorld.Kata.Greet();

            //for (int i = 0; i < 256; i++)
            //{
            //    Console.WriteLine($"{i}\t{(char)i}");
            //}

            //Console.WriteLine("Hello, World!");
        }
    }
}