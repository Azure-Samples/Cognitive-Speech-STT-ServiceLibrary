using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeechWithLuis.Src.Model
{
    public class ResponeModel
    {
        public string Text { get; set; }

        public string Intention { get; set; }

        public LuisIntent[] LuisIntents { get; set; }

        public LuisEntity[] LuisEntities { get; set; }
    }
}