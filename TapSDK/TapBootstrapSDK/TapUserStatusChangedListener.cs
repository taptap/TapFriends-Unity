namespace TapTap.Bootstrap
{
    public interface ITapUserStatusChangedListener
    {
        void OnLogout(TapError error);

        void OnBind(TapError error);
    }
}