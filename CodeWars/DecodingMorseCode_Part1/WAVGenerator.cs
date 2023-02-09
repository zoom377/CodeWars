using BenchmarkDotNet.Running;
using CommandLine.Text;
using Perfolizer.Horology;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWars.DecodingMorseCode_Part1
{
    public class WAVGenerator
    {

        static ushort ToLittleEndian(ushort val)
        {
            var bytes = BitConverter.GetBytes(val);
            bytes.Reverse();
            return BitConverter.ToUInt16(bytes);
        }

        static uint ToLittleEndian(uint val)
        {
            var bytes = BitConverter.GetBytes(val);
            bytes.Reverse();
            return BitConverter.ToUInt32(bytes);
        }

        /// <summary>
        /// <paramref name="start"/> and <paramref name="duration"/> are indexes into <paramref name="data"/>
        /// <para>Ideally they would specify start time and duration in seconds</para>
        /// </summary>
        static void InsertSinePitch(byte[] data, int start, int duration, int frequency)
        {
            double shortRate = 44_100; //sampleRate * channelCount * bitsPerSample / 16
            double amplitude = 10_000.0;

            for (int i = start; i < start + duration; i += 2)
            {
                double phase = Math.Sin(Math.PI / shortRate * i * frequency) * amplitude;
                short sample = (short)Math.Floor(phase);
                BitConverter.GetBytes(sample).CopyTo(data, i);
            }
        }

        static byte[] CreateWavFileMono16Bit(byte[] soundData)
        {
            //This code was way easier to write than messing around with a WAV struct representation.
            //Struct method had issues with the unknown length array at the end of WAV file.

            ushort typeFormat = 1;
            ushort channelCount = 1; //1 = mono, 2 = stereo
            ushort bitsPerSample = 16; //8 or 16
            uint sampleRate = 44_100;

            uint bytesPerSec = sampleRate * channelCount * bitsPerSample / 8;
            ushort blockAlignment = (ushort)(channelCount * bitsPerSample / 8);

            byte[] result = new byte[44 + soundData.Length];//Total size: 44 + soundData
            Encoding.ASCII.GetBytes("RIFF").CopyTo(result, 0);//"RIFF" file description header	4 bytes - FOURCC 0
            BitConverter.GetBytes(ToLittleEndian((uint)result.Length - 8)).CopyTo(result, 4);//size of file	4 bytes - DWORD 4
            Encoding.ASCII.GetBytes("WAVE").CopyTo(result, 8);//"WAVE" description header   4 bytes - FOURCC 8
            Encoding.ASCII.GetBytes("fmt ").CopyTo(result, 12);//"fmt " description header   4 bytes - FOURCC 12
            BitConverter.GetBytes(ToLittleEndian((uint)16)).CopyTo(result, 16);//size of WAVE section chunck 4 bytes - DWORD 16
            BitConverter.GetBytes(ToLittleEndian(typeFormat)).CopyTo(result, 20);//WAVE type format    2 bytes - WORD 20
            BitConverter.GetBytes(ToLittleEndian(channelCount)).CopyTo(result, 22);//mono / stereo 2 bytes - WORD 22
            BitConverter.GetBytes(ToLittleEndian(sampleRate)).CopyTo(result, 24);//sample rate 4 bytes - DWORD 24
            BitConverter.GetBytes(ToLittleEndian(bytesPerSec)).CopyTo(result, 28);//bytes / sec   4 bytes - DWORD 28
            BitConverter.GetBytes(ToLittleEndian(blockAlignment)).CopyTo(result, 32);//Block alignment 2 bytes - WORD 32
            BitConverter.GetBytes(ToLittleEndian(bitsPerSample)).CopyTo(result, 34);//Bits / sample 2 bytes - WORD 34
            Encoding.ASCII.GetBytes("data").CopyTo(result, 36);//"data" description header   4 bytes - FOURCC 36
            BitConverter.GetBytes(ToLittleEndian((uint)soundData.Length * bitsPerSample)).CopyTo(result, 40);//size of data chunk  4 bytes - DWORD 40
            soundData.CopyTo(result, 44);//data buffer 44

            return result;
        }

        /// <summary>
        /// Interprets 
        /// </summary>
        public static void Generate(string morseCode)
        {
            int timeUnit = 3500;
            var beeps = new List<int>();

            int currentAudioPos = 0;
            for (int i = 0; i < morseCode.Length; i++)
            {
                int interval = 0;
                if (morseCode[i] == '.')
                {
                    interval = timeUnit * 2;
                    beeps.Add(currentAudioPos);
                    beeps.Add(interval);
                    currentAudioPos += interval;
                }
                else if (morseCode[i] == '-')
                {
                    interval = timeUnit * 4;
                    beeps.Add(currentAudioPos);
                    beeps.Add(interval);
                    currentAudioPos += interval;
                }
                else if (morseCode[i] == ' ')
                {
                    interval = timeUnit * 2;
                }

                currentAudioPos += interval;
            }

            //bork
            int audioSize = beeps[beeps.Count - 2] + beeps[beeps.Count - 1] * 2;
            var audio = new byte[audioSize];
            int frequency = 800;
            for (int i = 0; i < beeps.Count; i+=2)
            {
                InsertSinePitch(audio, beeps[i], beeps[i+1], frequency);
            }


            var wav = CreateWavFileMono16Bit(audio);

            using (var file = File.Open("MorseCode.wav", FileMode.Create))
            {
                file.Write(wav);
            }

        }
    }
}
