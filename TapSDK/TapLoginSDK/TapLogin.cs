using System;

namespace TapTap.Login
{
    public class TapLogin
    {
        public static void GetProfile(Action<Profile> action)
        {
            TapLoginImpl.GetInstance().GetProfile(action);
        }

        public static void GetTapToken(Action<TapLoginToken> action)
        {
            TapLoginImpl.GetInstance().GetAccessToken(action);
        }
    }
}