using System;
using System.Collections.Generic;
using TapTap.Common;

namespace TapTap.Bootstrap
{
    public class TapError
    {
        public TapErrorCode code;

        public string errorDescription;

        public TapError(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return;
            }

            var dic = Json.Deserialize(json) as Dictionary<string, object>;
            var parseCode = SafeDictionary.GetValue<int>(dic, "code");
            code = ParseCode(parseCode);
            errorDescription = SafeDictionary.GetValue<string>(dic, "error_description");
        }

        public TapError()
        {
        }

        public static TapError SafeConstructorTapError(string json)
        {
            return string.IsNullOrEmpty(json) ? null : new TapError(json);
        }

        public static TapError UndefinedError()
        {
            return new TapError(TapErrorCode.ERROR_CODE_UNDEFINED, "UnKnown Error");
        }

        public TapError(int code, string errorDescription)
        {
            this.code = ParseCode(code);
            this.errorDescription = errorDescription;
        }

        private static TapErrorCode ParseCode(int parseCode)
        {
            return Enum.IsDefined(typeof(TapErrorCode), parseCode)
                ? (TapErrorCode) Enum.ToObject(typeof(TapErrorCode), parseCode)
                : TapErrorCode.ERROR_CODE_UNDEFINED;
        }

        public TapError(TapErrorCode code, string errorDescription)
        {
            this.code = code;
            this.errorDescription = errorDescription;
        }
    }
}