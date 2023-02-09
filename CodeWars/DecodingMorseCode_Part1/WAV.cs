using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWars.DecodingMorseCode_Part1
{
    //WAV file structure
    //Offset    Length      Contents
    //0         4 bytes     "RIFF"
    //4         4 bytes     <file length - 8>
    //8         4 bytes     "WAVE"
    //12        4 bytes     "fmt "
    //16        4 bytes     0x00000010     //Length of the fmt data (16 bytes)
    //20        2 bytes     0x0001         //Format tag: 1 = PCM
    //22        2 bytes     <channels>     //Channels: 1 = mono, 2 = stereo, or more
    //24        4 bytes     <sample rate>  //Samples per second: e.g., 44100
    //28        4 bytes     <bytes/second> //sample rate * block align
    //32        2 bytes     <block align>  //channels * bits/sample / 8
    //34        2 bytes     <bits/sample>  //Multiple of 8
    //36        4 bytes     "data"
    //40        4 bytes     <length of the data block>
    //44        ? bytes     <sample data>

    public class WAV
    {
        public ushort _channelCount { get; private set; } //1 = mono, 2 = stereo
        public ushort _bitsPerSample { get; private set; } //8 or 16
        public uint _sampleRate { get; private set; }

        /// <summary>
        /// Constructs from a valid pre-existing WAV file.
        /// </summary>
        public WAV(byte[] preExistingWAV)
        {
            if (preExistingWAV.Length < 44)
                throw new IndexOutOfRangeException("WAV file has invalid header!");

            _channelCount = BitConverter.ToUInt16(preExistingWAV, 22);
            _sampleRate = BitConverter.ToUInt16(preExistingWAV, 24);
            _bitsPerSample = BitConverter.ToUInt16(preExistingWAV, 34);
        }

        /// <summary>
        /// Constructs a new WAV object with given properties.
        /// </summary>
        public WAV(ushort channelCount, ushort bitsPerSample, ushort sampleRate)
        {
            _channelCount = channelCount;
            _bitsPerSample = bitsPerSample;
            _sampleRate = sampleRate;
        }

        public byte[] ToFile(byte[] soundData)
        {
            //This code was way easier to write than messing around with a WAV struct representation.
            //Struct method had issues with the unknown length array at the end of WAV file.

            byte[] result = new byte[44 + soundData.Length];//Total size: 44 + soundData
            ushort typeFormat = 1;
            ushort blockAlignment = (ushort)(_channelCount * _bitsPerSample / 8);
            uint bytesPerSec = _sampleRate * blockAlignment;

            Encoding.ASCII.GetBytes("RIFF").CopyTo(result, 0);//"RIFF" file description header	4 bytes - FOURCC 0
            BitConverter.GetBytes(ToLittleEndian((uint)result.Length - 8)).CopyTo(result, 4);//size of file	4 bytes - DWORD 4
            Encoding.ASCII.GetBytes("WAVE").CopyTo(result, 8);//"WAVE" description header   4 bytes - FOURCC 8
            Encoding.ASCII.GetBytes("fmt ").CopyTo(result, 12);//"fmt " description header   4 bytes - FOURCC 12
            BitConverter.GetBytes(ToLittleEndian((uint)16)).CopyTo(result, 16);//size of WAVE section chunck 4 bytes - DWORD 16
            BitConverter.GetBytes(ToLittleEndian(typeFormat)).CopyTo(result, 20);//WAVE type format    2 bytes - WORD 20
            BitConverter.GetBytes(ToLittleEndian(_channelCount)).CopyTo(result, 22);//mono / stereo 2 bytes - WORD 22
            BitConverter.GetBytes(ToLittleEndian(_sampleRate)).CopyTo(result, 24);//sample rate 4 bytes - DWORD 24
            BitConverter.GetBytes(ToLittleEndian(bytesPerSec)).CopyTo(result, 28);//bytes / sec   4 bytes - DWORD 28
            BitConverter.GetBytes(ToLittleEndian(blockAlignment)).CopyTo(result, 32);//Block alignment 2 bytes - WORD 32
            BitConverter.GetBytes(ToLittleEndian(_bitsPerSample)).CopyTo(result, 34);//Bits / sample 2 bytes - WORD 34
            Encoding.ASCII.GetBytes("data").CopyTo(result, 36);//"data" description header   4 bytes - FOURCC 36
            BitConverter.GetBytes(ToLittleEndian((uint)soundData.Length * _bitsPerSample)).CopyTo(result, 40);//size of data chunk  4 bytes - DWORD 40
            soundData.CopyTo(result, 44);//data buffer 44

            return result;
        }

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

        static void InsertSinePitch(byte[] data, int start, int duration, int frequency)
        {
            double shortRate = 44_100; //sampleRate * channelCount * bitsPerSample / 16

            for (int i = start; i < start + duration; i += 2)
            {
                double phase = Math.Sin(Math.PI / shortRate * i * frequency) * 10_000.0;
                short val = (short)Math.Floor(phase);
                BitConverter.GetBytes(val).CopyTo(data, i);
            }
        }
    }
}
