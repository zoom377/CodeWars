using BenchmarkDotNet.Running;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CodeWars.DecodingMorseCode_Part1
{
    public class WAVGenerator
    {
        //[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        //public struct WAVFileData
        //{
        //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        //    private byte[] RIFFDescriptionHeader;    //4
        //    public uint FileSize;                   //4 8
        //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        //    public byte[] WAVDescriptionHeader;     //4 12
        //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        //    public byte[] FormatDescriptionHeader;  //4 16  
        //    public uint SectionChunkSize;           //4 20  
        //    public ushort Format;                   //2 22
        //    public ushort MonoStereo;               //2 24
        //    public uint SampleFrequency;            //4 28
        //    public uint BytesPerSec;                //4 32
        //    public ushort BlockAlignment;           //2 34
        //    public ushort BitsPerSample;            //2 36
        //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        //    public byte[] DataDescriptionHeader;    //4 40
        //    public uint DataChunkSize;              //4 44
        //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0)]
        //    public byte[] Data;                     //?



        //    public WAVFileData(byte[] soundData)
        //    {
        //        RIFFDescriptionHeader = Encoding.UTF8.GetBytes("RIFF");
        //        WAVDescriptionHeader = Encoding.UTF8.GetBytes("WAVE");
        //        FormatDescriptionHeader = Encoding.UTF8.GetBytes("fmt ");
        //        DataDescriptionHeader = Encoding.UTF8.GetBytes("data");
        //        Data = soundData;
        //        SectionChunkSize = 16;
        //        Format = 0x01;
        //        MonoStereo= 0x01;
        //        SampleFrequency = 44100;
        //        BytesPerSec = 22050;
        //        BlockAlignment = 4;
        //        BitsPerSample = 500;
        //        DataChunkSize = 32;
        //        FileSize = 0;
        //    }



        //    public byte[] ToByteArray()
        //    {
        //        int size = Marshal.SizeOf(this);
        //        byte[] data = new byte[size];

        //        IntPtr ptr = IntPtr.Zero;
        //        try
        //        {
        //            ptr = Marshal.AllocHGlobal(size);
        //            Marshal.StructureToPtr(this, ptr, false);
        //            Marshal.Copy(ptr, data, 0, size);
        //        }
        //        finally
        //        {
        //            Marshal.FreeHGlobal(ptr);
        //        }

        //        return data;
        //    }
        //}

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

        static byte[] CreateWavFileMono16Bit(byte[] soundData)
        {
            ushort typeFormat = 1;
            ushort channelCount = 1; //1 = mono, 2 = stereo

            ushort bitsPerSample = 16; //8 or 16
            uint sampleRate = 44_100;
            ushort blockAlignment = (ushort)(channelCount * bitsPerSample / 8);
            uint bytesPerSec = sampleRate * blockAlignment;

            //Total size: 44 + soundData
            byte[] result = new byte[44 + soundData.Length];

            //"RIFF" file description header	4 bytes - FOURCC 0
            Encoding.ASCII.GetBytes("RIFF").CopyTo(result, 0);

            //size of file	4 bytes - DWORD 4
            BitConverter.GetBytes(ToLittleEndian((uint)result.Length - 8)).CopyTo(result, 4);

            //"WAVE" description header   4 bytes - FOURCC 8
            Encoding.ASCII.GetBytes("WAVE").CopyTo(result, 8);

            //"fmt " description header   4 bytes - FOURCC 12
            Encoding.ASCII.GetBytes("fmt ").CopyTo(result, 12);

            //size of WAVE section chunck 4 bytes - DWORD 16
            BitConverter.GetBytes(ToLittleEndian((uint)16)).CopyTo(result, 16);

            //WAVE type format    2 bytes - WORD 20
            BitConverter.GetBytes(ToLittleEndian(typeFormat)).CopyTo(result, 20);

            //mono / stereo 2 bytes - WORD 22
            BitConverter.GetBytes(ToLittleEndian(channelCount)).CopyTo(result, 22);

            //sample rate 4 bytes - DWORD 24
            BitConverter.GetBytes(ToLittleEndian(sampleRate)).CopyTo(result, 24);

            //bytes / sec   4 bytes - DWORD 28
            BitConverter.GetBytes(ToLittleEndian(bytesPerSec)).CopyTo(result, 28);

            //Block alignment 2 bytes - WORD 32
            BitConverter.GetBytes(ToLittleEndian(blockAlignment)).CopyTo(result, 32);

            //Bits / sample 2 bytes - WORD 34
            BitConverter.GetBytes(ToLittleEndian(bitsPerSample)).CopyTo(result, 34);

            //"data" description header   4 bytes - FOURCC 36
            Encoding.ASCII.GetBytes("data").CopyTo(result, 36);

            //size of data chunk  4 bytes - DWORD 40
            BitConverter.GetBytes(ToLittleEndian((uint)soundData.Length)).CopyTo(result, 40);

            //Data Unspecified data buffer 44
            soundData.CopyTo(result, 44);

            return result;
        }

        public static void Generate(string morseCode)
        {
            var wav = CreateWavFileMono16Bit(new byte[] { });

            using (var file = File.Open("MorseCode.wav", FileMode.Create))
            {
                foreach (var b in wav)
                {
                    Console.Write(b);
                }

                file.Write(wav);
            }

            var x = 0;
        }
    }
}
