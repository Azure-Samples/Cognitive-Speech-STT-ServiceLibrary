using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechConsole.PcmConverter
{
    public class Pcm2Wav
    {
        public static void WriteInt2Bytes(int value, List<byte> outs)
        {
            // Audio data (conversion big endian -> little endian)
            var temp = BitConverter.GetBytes(value);
            outs.Add(temp[0]);
            outs.Add(temp[1]);
            outs.Add(temp[2]);
            outs.Add(temp[3]);
        }

        public static void WriteShort2Bytes(short value, List<byte> outs)
        {
            // Audio data (conversion big endian -> little endian)
            var temp = BitConverter.GetBytes(value);
            outs.Add(temp[0]);
            outs.Add(temp[1]);
        }

        public static void WriteString2Bytes(string value, List<byte> outs)
        {
            // Audio data (conversion big endian -> little endian)
            foreach(var ch in value.ToCharArray())
            {
                outs.Add(Convert.ToByte(ch));
            }
        }



        private List<byte> _forByteConvert;

        private byte[] _pcmAudioBytes;

        private int _pcmLength;

        private int _headerLen;

        private int _sampleRate = 16000;

        private int _bitsPerSample = 16;

        private int _numOfChannel = 1;

        public Pcm2Wav(byte[] pcmAudioBytes)
        {
            _forByteConvert = new List<byte>();
            _pcmAudioBytes = pcmAudioBytes;
            _pcmLength = this._pcmAudioBytes.Length;
        }

        // for your refenrece http://stackoverflow.com/questions/27826593/androidcreating-wave-file-using-raw-pcm-the-wave-file-does-not-play
        // http://stackoverflow.com/questions/18406010/how-to-convert-pcm-data-to-wav-file

        private byte[] GetWavHeader()
        {
            WriteString2Bytes("RIFF", this._forByteConvert); // chunk id
            WriteInt2Bytes( 36 + _pcmLength, this._forByteConvert); // // chunk size

            WriteString2Bytes("WAVE", this._forByteConvert); // format
            WriteString2Bytes("fmt ", this._forByteConvert); // subchunk 1 id

            WriteInt2Bytes(16, this._forByteConvert); // subchunk 1 size
            WriteShort2Bytes((short)1, this._forByteConvert); // audio format (1 = PCM)

            WriteShort2Bytes((short)_numOfChannel, this._forByteConvert); // number of channels
            WriteInt2Bytes(_sampleRate, this._forByteConvert); // sample rate

            WriteInt2Bytes((_sampleRate * _numOfChannel * _bitsPerSample) / 8 , this._forByteConvert); // ByteRate
            WriteShort2Bytes((short)2, this._forByteConvert); // block align

            WriteShort2Bytes((short)_bitsPerSample, this._forByteConvert); // bits per sample
            WriteString2Bytes("data", this._forByteConvert); // subchunk 2 id

            WriteInt2Bytes(_pcmLength, this._forByteConvert); // subchunk 2 size

            _headerLen = this._forByteConvert.Count();

            return this._forByteConvert.ToArray();

        }


        private byte[] GetWaveBytes()
        {
            var headerBytes = this.GetWavHeader();
            var outs = new byte[_headerLen + _pcmLength];
            Array.Copy(headerBytes, 0, outs, 0, _headerLen - 1);
            Array.Copy(_pcmAudioBytes, 0, outs, _headerLen, _pcmLength - 1);
            return outs;
        }

        public byte[] WavBytes
        {
            get {
                return GetWaveBytes();
            }
            set { }
        }

        public int WavLength
        {
            get
            {
                return _headerLen + _pcmLength;
            }
            set { }
        }

    }
}
