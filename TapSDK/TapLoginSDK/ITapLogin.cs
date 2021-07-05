using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using TapTap.Common;

namespace TapTap.Login
{
    public interface ITapLogin
    {
        void ChangeConfig(bool roundCorner, bool isPortrait);

        Task<Profile> FetchProfile();

        Task<Profile> GetProfile();

        Task<AccessToken> GetAccessToken();

        Task<AccessToken> Login();
    }

    public interface ITapLoginResultListener
    {
        void LoginCancel();

        void LoginError(TapError error);

        void LoginSuccess(AccessToken token);
    }

    public static class TapLoginConstants
    {
        public static string TAP_LOGIN_SERVICE = "TDSLoginService";

        public static string TAP_LOGIN_SERVICE_CLZ = "com.taptap.sdk.wrapper.TDSLoginService";

        public static string TAP_LOGIN_SERVICE_IMPL = "com.taptap.sdk.wrapper.TDSLoginServiceImpl";
    }
}