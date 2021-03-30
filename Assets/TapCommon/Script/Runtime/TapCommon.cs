using System;

namespace TapCommon
{
    public class TapCommon
    {
        public static void SetLanguage(string language)
        {
            TapCommonImpl.GetInstance().SetLanguage(language);
        }

        public static void GetRegionCode(Action<bool> callback)
        {
            TapCommonImpl.GetInstance().GetRegionCode(callback);
        }
    }
}