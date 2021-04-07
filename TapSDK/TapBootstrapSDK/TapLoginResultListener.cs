namespace TapTap.Bootstrap
{
    public interface ITapLoginResultListener
    {
        void OnLoginSuccess(AccessToken token);

        void OnLoginCancel();

        void OnLoginError(TapError error);
    }
}