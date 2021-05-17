using System;

namespace TapTap.Bootstrap
{
    public interface ITapBootstrap
    {
        void Init(TapConfig config);

        [Obsolete("已弃用,请调用Login(string[] permissions)")]
        void Login(LoginType loginType, string[] permissions);

        [Obsolete("已弃用,请调用Login(string[] permissions)")]
        void Bind(LoginType loginType, string[] permissions);

        void Login(string[] permissions);

        void GetTestQualification(Action<bool, TapError> action);
        
        void RegisterUserStatusChangedListener(ITapUserStatusChangedListener listener);

        void RegisterLoginResultListener(ITapLoginResultListener listener);

        void GetUser(Action<TapUser, TapError> action);

        void GetDetailUser(Action<TapUserDetail, TapError> action);

        void GetAccessToken(Action<AccessToken, TapError> action);

        void Logout();
        
        [Obsolete("已弃用")]
        void OpenUserCenter();

        void SetPreferLanguage(TapLanguage tapLanguage);
    }
}