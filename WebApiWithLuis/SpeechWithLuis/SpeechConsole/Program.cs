// http://www.c-sharpcenter.com/Tutorial/UnManaged.htm


using System.Runtime.InteropServices;
using System;
using SpeechConsole.FileOperation;

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
        int* length
     );

    public static void Main()
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
        unsafe
        {
            short* buffer_y;
            Int32 length;
            SilkDecoderToPcm(bytes, len, &buffer_y, &length);
            short[] buffers = new short[length];
            //Marshal.Copy(Marshal.AllocHGlobal(buffer_y[0]), buffers, 0 , length);
            for(int i=0; i< length; i++)
            {
                buffers[i] = buffer_y[i];
            }
            PcmWriter.WriteShorts(buffers);
        }

        Console.WriteLine("buffer: {0}", buffer[0]);
        Console.WriteLine("Return Value: " + ret);
        Console.WriteLine("String filled in DLL: " + System.Text.Encoding.ASCII.GetString(string_filled_in_dll));

    }
}