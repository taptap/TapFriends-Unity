using LeanCloud;

namespace TapTap.Bootstrap
{
    public class TapStorageStartTask : IStartTask
    {
        public void Invoke(TapConfig config)
        {
            LCApplication.Initialize(config.ClientID, config.ClientToken, config.ServerURL);
        }
    }
}