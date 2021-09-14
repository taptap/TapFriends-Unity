using System;

namespace TapTap.Desk
{
    public class TapDeskException : Exception
    {
        public int Code { get; set; }

        public new string Message { get; set; }

        public TapDeskException(int code, string message)
        {
            this.Code = code;
            this.Message = message;
        }

        public override string ToString() => $"{Code} - {Message}";
    }
}