using FragLabs.Audio.Codecs;
using SpeechWithLuis.Src.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeechWithLuis.Src.Services
{
    public class OpusService : IDisposable
    {
        private OpusDecoder decoder;

        public OpusService()
        {
            this.decoder = OpusDecoder.Create(48000, 1);
        }

        public OpusService(int outputSampleRate, int outputChannels)
        {
            this.decoder = OpusDecoder.Create(outputSampleRate, outputChannels);
        }

        public OutPutOpusData GetWavFormatAudio(InputOpusData data)
        {
            int codedlength = 0;
            var codedData =  decoder.Decode(data.MetaData, data.DataLength, out codedlength);
            return new OutPutOpusData
            {
                DecodedData = codedData,
                DataLength = codedlength
            };
        }

        void IDisposable.Dispose()
        {
            decoder.Dispose();
            decoder = null;
        }
    }
}