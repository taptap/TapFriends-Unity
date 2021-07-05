using System;
using TapTap.Common;

namespace TapTap.Bootstrap
{
    [Obsolete("已弃用")]
    public interface ITapUserStatusChangedListener
    {
        void OnLogout(TapError error);

        void OnBind(TapError error);
    }
}