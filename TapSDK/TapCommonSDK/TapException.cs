using System;

namespace TapTap.Common
{
    public class TapException : Exception
    {
        public int code;

        public string message;

        public TapException(int code, string message)
        {
            this.code = code;
            this.message = message;
        }
    }
}