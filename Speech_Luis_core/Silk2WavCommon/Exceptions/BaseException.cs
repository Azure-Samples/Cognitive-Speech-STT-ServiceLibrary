using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silk2WavCommon.Exceptions
{
    public class BaseException : Exception
    {
        protected int errorCode = 0;

        public BaseException() { }

        public BaseException(int code, string message) : base(message)
        {
            errorCode = code;
        }

        public BaseException(int code, string message, Exception inner) : base(message, inner)
        {
            errorCode = code;
        }

        public int ErrorCode
        {
            get
            {
                return errorCode;
            }
        }
    }
}
