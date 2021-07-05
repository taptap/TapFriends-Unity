using System.Threading.Tasks;

namespace TapTap.Login
{
    public class TapLogin
    {
        public static void ChangeConfig(bool roundCorner, bool isPortrait)
        {
            TapLoginImpl.GetInstance().ChangeConfig(roundCorner, isPortrait);
        }

        public static Task<Profile> FetchProfile()
        {
            return TapLoginImpl.GetInstance().FetchProfile();
        }

        public static Task<Profile> GetProfile()
        {
            return TapLoginImpl.GetInstance().GetProfile();
        }

        public static Task<AccessToken> GetAccessToken()
        {
            return TapLoginImpl.GetInstance().GetAccessToken();
        }

        public static Task<AccessToken> Login()
        {
            return TapLoginImpl.GetInstance().Login();
        }
    }
}