using System;
using System.Collections.Generic;
using TapCommon;

namespace TapBootstrap
{
    public class TapError
    {
        public TapErrorCode code;

        public string errorDescription;

        public TapError(string json)
        {
            Dictionary<string, object> dic = Json.Deserialize(json) as Dictionary<string, object>;
            int parseCode = SafeDictionary.GetValue<int>(dic, "code");
            this.code = ParseCode(parseCode);
            this.errorDescription = SafeDictionary.GetValue<string>(dic, "error_description");
        }

        public TapError()
        {
            
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

        public TapErrorCode ParseCode(int parseCode)
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