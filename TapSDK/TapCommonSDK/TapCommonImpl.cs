using System;
using System.Collections.Generic;

namespace TapTap.Common
{
    public class TapCommonImpl : ITapCommon
    {
        private static TapCommonImpl sInstance = new TapCommonImpl();

        public static TapCommonImpl GetInstance()
        {
            return sInstance;
        }

        private TapCommonImpl()
        {
            EngineBridge.GetInstance().Register("com.tds.common.wrapper.TDSCommonService", "com.tds.common.wrapper.TDSCommonServiceImpl");
        }

        public void SetLanguage(string language)
        {
            var dic = new Dictionary<string, object> {{"language", language}};
            var command = new Command("TDSCommonService", "setLanguage", false, dic);
            EngineBridge.GetInstance().CallHandler(command);
        }

        public void GetRegionCode(Action<bool> callback)
        {
            var command = new Command.Builder()
                .Service("TDSCommonService")
                .Method("getRegionCode").Callback(true)
                .OnceTime(true)
                .CommandBuilder();
            EngineBridge.GetInstance().CallHandler(command, (result) =>
            {
                if (result.code != Result.RESULT_SUCCESS)
                {
                    return;
                }

                if (string.IsNullOrEmpty(result.content))
                {
                    return;
                }

                var wrapper = new CommonRegionWrapper(result.content);
                callback(wrapper.isMainland);
            });

        }

    }

}