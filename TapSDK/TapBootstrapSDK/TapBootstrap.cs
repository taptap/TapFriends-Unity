using System;

namespace TapTap.Bootstrap
{
    public class TapBootstrap
    {
        public static void Init(TapConfig tapConfig)
        {
            TapBootstrapImpl.GetInstance().Init(tapConfig);
        }

        public static void Login(LoginType loginType, string[] permissions)
        {
            TapBootstrapImpl.GetInstance().Login(loginType, permissions);
        }

        public static void Bind(LoginType loginType, string[] permissions)
        {
            TapBootstrapImpl.GetInstance().Bind(loginType, permissions);
        }

        public static void RegisterUserStatusChangedListener(ITapUserStatusChangedListener listener)
        {
            TapBootstrapImpl.GetInstance().RegisterUserStatusChangedListener(listener);
        }

        public static void RegisterLoginResultListener(ITapLoginResultListener listener)
        {
            TapBootstrapImpl.GetInstance().RegisterLoginResultListener(listener);
        }

        public static void GetUser(Action<TapUser, TapError> action)
        {
            TapBootstrapImpl.GetInstance().GetUser(action);
        }

        public static void GetDetailUser(Action<TapUserDetail, TapError> action)
        {
            TapBootstrapImpl.GetInstance().GetDetailUser(action);
        }

        public static void GetAccessToken(Action<AccessToken, TapError> action)
        {
            TapBootstrapImpl.GetInstance().GetAccessToken(action);
        }

        public static void Logout()
        {
            TapBootstrapImpl.GetInstance().Logout();
        }

        public static void OpenUserCenter()
        {
            TapBootstrapImpl.GetInstance().OpenUserCenter();
        }

        public static void SetPreferLanguage(TapLanguage tapLanguage)
        {
            TapBootstrapImpl.GetInstance().SetPreferLanguage(tapLanguage);
        }
    }
}