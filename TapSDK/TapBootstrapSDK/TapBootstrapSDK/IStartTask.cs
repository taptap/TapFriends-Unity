using TapTap.Common;

namespace TapTap.Bootstrap
{
    public interface IStartTask
    {
        void Invoke(TapConfig config);
    }
}