using LeanCloud;
using LeanCloud.Storage;
using TapTap.Common;

namespace TapTap.Bootstrap
{
    public class TapStorageStartTask : IStartTask
    {
        public void Invoke(TapConfig config)
        {
            LCApplication.Initialize(config.ClientID, config.ClientToken, config.ServerURL);
            LCObject.RegisterSubclass("_User", () => new TDSUser());
            LCObject.RegisterSubclass(TapGameSave.CLASS_NAME, () => new TapGameSave());
        }
    }
}