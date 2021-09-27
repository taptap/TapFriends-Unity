using System;

namespace TapTap.Support
{
    public class TapSupportException : Exception
    {
        public int Code { get; set; }

        public new string Message { get; set; }

        public TapSupportException(int code, string message)
        {
            this.Code = code;
            this.Message = message;
        }

        public override string ToString() => $"{Code} - {Message}";
    }
}