using System;

namespace TapBootstrapSDK
{
    public interface ITapBootstrap
    {
        void Init(TapConfig config);

        void Login(LoginType loginType, string[] permissions);

        void Bind(LoginType loginType, string[] permissions);

        void RegisterUserStatusChangedListener(ITapUserStatusChangedListener listener);

        void RegisterLoginResultListener(ITapLoginResultListener listener);

        void GetUser(Action<TapUser, TapError> action);

        void GetDetailUser(Action<TapUserDetail, TapError> action);

        void GetAccessToken(Action<AccessToken, TapError> action);

        void Logout();

        void OpenUserCenter();

        void SetPreferLanguage(TapLanguage tapLanguage);
    }
}