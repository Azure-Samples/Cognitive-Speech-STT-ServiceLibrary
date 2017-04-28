using Silk2WavCommon.Exceptions;
using System;

namespace SpeechLuisOwin.Src.Exceptions
{
    public class SpeechException : BaseException
    {
        public SpeechException() { }

        public SpeechException(int code, string message) : base(code, message)
        {
        }

        public SpeechException(int code, string message, Exception inner) : base(code, message, inner)
        {
        }
    }
}