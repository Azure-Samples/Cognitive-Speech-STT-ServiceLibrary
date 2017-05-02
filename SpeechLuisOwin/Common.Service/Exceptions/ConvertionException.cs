using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Service.Exceptions
{
    public class ConvertionException : BaseException
    {
        public ConvertionException(){}

        public ConvertionException(int code, string message) : base(code, message)
        {
        }

        public ConvertionException(int code, string message, Exception inner) : base(code, message, inner)
        {
        }
    }
}
