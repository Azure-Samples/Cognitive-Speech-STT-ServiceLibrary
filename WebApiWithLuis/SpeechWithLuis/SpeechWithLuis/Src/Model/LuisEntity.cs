using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeechWithLuis.Src.Model
{
    public class LuisEntity
    {
        public string Entity { get; set; }

        public string type { get; set; }

        public int startIndex { get; set; }

        public int endIndex { get; set; }

        public float score { get; set; }
    }
}