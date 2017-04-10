using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeechWithLuis.Src.Model
{
    public class ResponeModel
    {
        public string Text { get; set; }

        public dynamic intentions { get; set; }

        public long GetAudioTextLantency { get; set; }

        public long GetAudioIntentionLantency { get; set; }

        public string ExceptionString { get; set;}

        public int ErrorCode { get; set; }

        public DateTime ArrivalTime { get; set; }

        public DateTime EndTime { get; set; }

    }
}