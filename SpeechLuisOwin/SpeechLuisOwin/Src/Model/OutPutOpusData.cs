using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeechLuisOwin.Src.Model
{
	public class OutPutOpusData
	{
        public byte[] DecodedData { get; set; }

        public int DataLength { get; set; }
    }
}