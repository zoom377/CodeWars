using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWars.DecodingMorseCode_Part1
{
    [MemoryDiagnoser]
    [DisassemblyDiagnoser]
    [KeepBenchmarkFiles]
    public class Benchmarks
    {

        [Params(
            "I LIKE TO PLAY GUITAR", 
            " WAY   TOO      MUCH CODING   ",
            "PUSS FISH PUSS FISH PUSS FISH PUSS FISH PUSS FISH PUSS FISH PUSS FISH PUSS FISH",
            "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               stealth                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            ",
            "Lorem Ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
            )]
        public string DecodeParams { get; set; }

        [Benchmark]
        public void Decode() => MorseCodeDecoder.Decode(DecodeParams);

        //[Benchmark]
        //public void Decode() => MorseCodeDecoder.DecodeOld(".... . -.--   .--- ..- -.. .");

        //[Benchmark]
        //public void DecodeDict() => MorseCodeDecoder.DecodeDict(".... . -.--   .--- ..- -.. .");

    }
}
