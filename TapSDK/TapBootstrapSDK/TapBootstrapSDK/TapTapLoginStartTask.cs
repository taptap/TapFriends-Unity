using TapTap.Common;
using TapTap.Login;

namespace TapTap.Bootstrap
{
    public class TapTapLoginStartTask : IStartTask
    {
        public void Invoke(TapConfig config)
        {
            TapLogin.Init(config.ClientID, config.RegionType == RegionType.CN, true);
        }
    }
}