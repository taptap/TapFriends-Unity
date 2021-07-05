using System;
using TapTap.Common;

namespace TapTap.Bootstrap
{
    [Obsolete("已弃用")]
    public interface ITapLoginResultListener
    {
        void OnLoginSuccess(AccessToken token);

        void OnLoginCancel();

        void OnLoginError(TapError error);
    }
}