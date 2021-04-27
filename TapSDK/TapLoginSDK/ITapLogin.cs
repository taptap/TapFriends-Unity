using System;

namespace TapTap.Login
{
    public interface ITapLogin
    {
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