using TapTap.Common;

namespace TapTap.Bootstrap
{
    public class TapCommonStartTask:IStartTask
    {
        public TapCommonStartTask()
        {
        }

        public void Invoke(TapConfig config)
        {
            TapCommon.Init(config);
        }
    }
}