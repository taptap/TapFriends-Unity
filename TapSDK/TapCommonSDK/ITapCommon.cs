using System;
using System.Collections.Generic;

namespace TapCommonSDK
{
    public interface ITapCommon
    {
        void SetLanguage(string language);

        void GetRegionCode(Action<bool> callback);
    }
    
    [Serializable]
    public class CommonRegionWrapper
    {
        public bool isMainland;

        public CommonRegionWrapper(string json)
        {
            Dictionary<string,object> dic = Json.Deserialize(json) as Dictionary<string,object>;
            this.isMainland = SafeDictionary.GetValue<bool>(dic,"isMainland");
        }

    }
}