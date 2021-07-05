using System;
using TapTap.Common;

namespace TapTap.Bootstrap
{
    public class TapBootstrap
    {
        public static void Init(TapConfig tapConfig)
        {
            TapBootstrapImpl.GetInstance().Init(tapConfig);
        }

        [Obsolete("已弃用")]
        public static void Login(LoginType loginType, string[] permissions)
        {
            // TapBootstrapImpl.GetInstance().Login(loginType, permissions);
        }

        [Obsolete("已弃用")]
        public static void Bind(LoginType loginType, string[] permissions)
        {
            // TapBootstrapImpl.GetInstance().Bind(loginType, permissions);
        }

        [Obsolete("已弃用")]
        public static void GetTestQualification(Action<bool, TapError> action)
        {
            // TapBootstrapImpl.GetInstance().GetTestQualification(action);
        }

        [Obsolete("已弃用")]
        public static void RegisterUserStatusChangedListener(ITapUserStatusChangedListener listener)
        {
            // TapBootstrapImpl.GetInstance().RegisterUserStatusChangedListener(listener);
        }

        [Obsolete("已弃用")]
        public static void RegisterLoginResultListener(ITapLoginResultListener listener)
        {
            // TapBootstrapImpl.GetInstance().RegisterLoginResultListener(listener);
        }

        [Obsolete("已弃用")]
        public static void GetUser(Action<TapUser, TapError> action)
        {
            // TapBootstrapImpl.GetInstance().GetUser(action);
        }

        [Obsolete("已弃用")]
        public static void GetDetailUser(Action<TapUserDetail, TapError> action)
        {
            // TapBootstrapImpl.GetInstance().GetDetailUser(action);
        }

        [Obsolete("已弃用")]
        public static void GetAccessToken(Action<AccessToken, TapError> action)
        {
            // TapBootstrapImpl.GetInstance().GetAccessToken(action);
        }

        [Obsolete("已弃用")]
        public static void Logout()
        {
            // TapBootstrapImpl.GetInstance().Logout();
        }

        [Obsolete("已弃用")]
        private static void OpenUserCenter()
        {
            // TapBootstrapImpl.GetInstance().OpenUserCenter();
        }

        [Obsolete("已弃用")]
        public static void SetPreferLanguage(TapLanguage tapLanguage)
        {
            // TapBootstrapImpl.GetInstance().SetPreferLanguage(tapLanguage);
        }
    }
}