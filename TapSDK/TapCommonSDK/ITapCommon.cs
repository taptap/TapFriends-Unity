using System;
using System.Collections.Generic;

namespace TapTap.Common
{
    public interface ITapCommon
    {
        void SetLanguage(string language);

        void GetRegionCode(Action<bool> callback);
        
        void IsTapTapInstalled(Action<bool> callback);

        void IsTapTapGlobalInstalled(Action<bool> callback);

        void UpdateGameInTapTap(string appId, Action<bool> callback);

        void UpdateGameInTapGlobal(string appId, Action<bool> callback);

        void OpenReviewInTapTap(string appId, Action<bool> callback);

        void openReviewInTapGlobal(string appId, Action<bool> callback);
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