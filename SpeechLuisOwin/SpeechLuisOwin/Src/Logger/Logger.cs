using Microsoft.Owin.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace SpeechLuisOwin.Src.Logger
{
    public class Logger : ILogger
    {
        bool ILogger.WriteCore(TraceEventType eventType, int eventId, object state, Exception exception, Func<object, Exception, string> formatter)
        {
            throw new NotImplementedException();
        }
    }
}