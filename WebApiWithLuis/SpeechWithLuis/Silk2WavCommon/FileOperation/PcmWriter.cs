using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silk2WavCommon.FileOperation
{
    public class PcmWriter
    {
        public static void WriteBytes2SpecificFile(byte[] values, string myPath)
        {
            using (FileStream fs = new FileStream(myPath, FileMode.Create, FileAccess.Write))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    foreach (var value in values)
                    {
                        bw.Write(value);
                    }
                }
            }
        }

        public static void WriteShorts2SpecificFile(byte[] values, string myPath)
        {
            using (FileStream fs = new FileStream(myPath, FileMode.CreateNew, FileAccess.Write))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    foreach (short value in values)
                    {
                        bw.Write(value);
                    }
                }
            }
        }
    }
}
