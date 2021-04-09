using System;
using System.Collections.Generic;
using TapTap.Common;

namespace TapTap.Friends
{
    public class TapError
    {
        public int code;
        public string errorDescription;

        public TapError(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return;
            }
            var dic = Json.Deserialize(json) as Dictionary<string, object>;
            code = SafeDictionary.GetValue<int>(dic, "code");
            errorDescription = SafeDictionary.GetValue<string>(dic, "error_description");
        }

        public TapError(int code, string errorDescription)
        {
            this.code = code;
            this.errorDescription = errorDescription;
        }

        public static TapError SafeConstrucTapError(string json)
        {
            return string.IsNullOrEmpty(json) ? null : new TapError(json);
        }

        private static TapErrorCode ParseCode(int parseCode) {
            return Enum.IsDefined(typeof(TapErrorCode), parseCode)
                ? (TapErrorCode) Enum.ToObject(typeof(TapErrorCode), parseCode)
                : TapErrorCode.ERROR_CODE_UNDEFINED;
        }
    }
}













