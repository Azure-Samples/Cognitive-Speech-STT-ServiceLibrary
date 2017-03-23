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

        public string ArrivalTime { get; set; }

        public string EndTime { get; set; } 
    }
}