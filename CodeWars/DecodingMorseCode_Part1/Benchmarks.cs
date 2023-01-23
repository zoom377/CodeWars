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
    public class Benchmarks
    {
        [Benchmark]
        public void Decode() => MorseCodeDecoder.Decode(".... . -.--   .--- ..- -.. .");

        [Benchmark]
        public void DecodeDict() => MorseCodeDecoder.DecodeDict(".... . -.--   .--- ..- -.. .");

    }
}
