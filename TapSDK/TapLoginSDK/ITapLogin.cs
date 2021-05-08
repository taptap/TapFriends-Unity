using System;

namespace TapTap.Login
{
    public interface ITapLogin
    {
        void ChangeConfig(bool roundCorner, bool isPortrait);

        void GetProfile(Action<Profile> action);

        void GetAccessToken(Action<TapLoginToken> action);
    }

    public static class TapLoginConstants
    {
        public static string TAP_LOGIN_SERVICE = "TDSLoginService";

        public static string TAP_LOGIN_SERVICE_CLZ = "com.taptap.sdk.wrapper.TDSLoginService";

        public static string TAP_LOGIN_SERVICE_IMPL = "com.taptap.sdk.wrapper.TDSLoginServiceImpl";
    }
}