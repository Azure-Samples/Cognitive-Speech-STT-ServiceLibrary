// http://www.c-sharpcenter.com/Tutorial/UnManaged.htm


using System.Runtime.InteropServices;
using System;
using SpeechConsole.FileOperation;
using SpeechConsole.Web;
using SpeechConsole.PcmConverter;
using Silk2WavCommon.Silk2WavConverter;
using System.Diagnostics;

class call_dll
{

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct STRUCT_DLL
    {
        public Int32 count_int;
        public IntPtr ints;
    }

    [DllImport("mingw_dll.dll", CallingConvention = CallingConvention.Cdecl)]
    private static extern int func_dll(
        int an_int,
        [MarshalAs(UnmanagedType.LPArray)] byte[] string_filled_in_dll,
        ref STRUCT_DLL s
     );

    [DllImport("decoderdll.dll", CallingConvention = CallingConvention.Cdecl)]
    private unsafe static extern int GetResult(
        int an_int,
        byte** buffer 
     );

    [DllImport("decoderdll.dll", CallingConvention = CallingConvention.Cdecl)]
    private unsafe static extern int SilkDecoderToPcm(
        byte[] jBuffers,
        int size,
        short** outBuffer,
        int* length,
        int sampleRate
     );

    public static void Main()
    {


       // WavAudioTest();
        //WavAudioTest();
        LatencyTest();
        //var test = finallyBlock();
        //test = 3;
        //bak();
    }

    static int finallyBlock()
    {
        int i = 0;
        try
        {
            return i;
        }
        finally
        {
            i = 5;
            //return 0;
        }
    }


    static void LatencyTest()
    {
        var bytes = PcmReader.GetFileBytes("phpRX7A4h.silk");
        var len = bytes.GetLength(0);
        while (true)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            //var silk2Wav = new Silk2Wav(bytes, len);
            var outs = AudioPost.SendAudioFile(bytes, len);
            stopWatch.Stop();
            Console.WriteLine(outs + " + timeSpan: " + stopWatch.ElapsedMilliseconds);
        }
    }

    static void WavAudioTest()
    {
        var bytes = PcmReader.GetFileBytes("phpRX7A4h.silk");
        var len = bytes.GetLength(0);
        var silk2Wav = new Silk2Wav(bytes, len);
        PcmWriter.WriteBytes2SpecificFile(silk2Wav.WavBytes, "j8.wav");
    }


    static void bak()
    {
        byte[] string_filled_in_dll = new byte[21];


        STRUCT_DLL struct_dll = new STRUCT_DLL();
        struct_dll.count_int = 5;
        int[] ia = new int[5];
        ia[0] = 2; ia[1] = 3; ia[2] = 5; ia[3] = 8; ia[4] = 13;
        byte[] buffer = {
            Convert.ToByte('a'),
            Convert.ToByte('a'),
            Convert.ToByte('a'),
            Convert.ToByte('a') };


        GCHandle gch = GCHandle.Alloc(ia);
        struct_dll.ints = Marshal.UnsafeAddrOfPinnedArrayElement(ia, 0);

        //int ret = func_dll(5, string_filled_in_dll, ref struct_dll);
        int ret;
        unsafe
        {
            byte* buffer_x;
            ret = GetResult(5, &buffer_x);
        }

        var bytes = PcmReader.GetFileBytes();
        var len = bytes.GetLength(0);
        byte[] buffers;
        int count = 0;
        unsafe
        {
            short* buffer_y;
            Int32 length;
            SilkDecoderToPcm(bytes, len, &buffer_y, &length, 16000);
            buffers = new byte[length * 2];
            //Marshal.Copy(Marshal.AllocHGlobal(buffer_y[0]), buffers, 0 , length);
            for (int i = 0; i < length; i++)
            {
                var temps = BitConverter.GetBytes(buffer_y[i]);
                buffers[i * 2] = temps[0];
                buffers[i * 2 + 1] = temps[1];
            }
            PcmWriter.WriteBytes(buffers);
            count = length * 2;
        }
        /*
        var mybytes = PcmReader.GetFileBytesViaPath("phpBV6Z6a.wav");
        var mylen = mybytes.GetLength(0);
        */
        var wav = new Pcm2Wav(buffers);
        //PcmWriter.WriteBytes2SpecificFile(wav.WavBytes, "test.wav");
        var outs = AudioPost.SendAudioFile(wav.WavBytes, wav.WavLength);



        Console.WriteLine("buffer: {0}", buffer[0]);
        Console.WriteLine("Return Value: " + ret);
        Console.WriteLine("String filled in DLL: " + System.Text.Encoding.ASCII.GetString(string_filled_in_dll));
    }
}