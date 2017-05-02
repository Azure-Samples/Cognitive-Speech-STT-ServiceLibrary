using System;

namespace Common.Service.Exceptions
{
    public class LuisException : BaseException
    {
        public LuisException() { }

        public LuisException(int code, string message) : base(code, message)
        { 
        }

        public LuisException(int code, string message, Exception inner) : base(code, message, inner)
        {
        }
    }
}