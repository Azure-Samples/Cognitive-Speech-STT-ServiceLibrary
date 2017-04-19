using Silk2WavCommon.PcmConverter;
using Silk2WavCommon.SilkConverter;

namespace Silk2WavCommon.Silk2WavConverter
{
    public class Silk2Wav
    {

        private byte[] _silkBytes;

        private int _silkbyteLen;

        private byte[] _wavBytes;

        private int _wavBytesLen;

        public Silk2Wav(byte[] silkBytes, int silkbyteLen)
        {
            this._silkBytes = silkBytes;
            this._silkbyteLen = silkbyteLen;

            // decoded to pcm byte array
            var decoder = new SilkDecoder(silkBytes, silkbyteLen);
            var pcmBytes = decoder.PcmOuts;
            var pcmBytesLen = decoder.PcmOutsLen;

            // pcm byte to wav byte array
            var pcm = new Pcm2Wav(pcmBytes);
            this._wavBytes = pcm.WavBytes;
            this._wavBytesLen = pcm.WavLength;
        }


        public byte[] WavBytes
        {
            get
            {
                return _wavBytes;
            }
            set
            {
            }
        }

        public int WavBytesLen
        {
            get
            {
                return _wavBytesLen;
            }
        }
    }
}
