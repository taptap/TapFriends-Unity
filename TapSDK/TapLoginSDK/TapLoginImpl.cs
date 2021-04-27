using System;
using TapTap.Common;

namespace TapTap.Login
{
    public class TapLoginImpl : ITapLogin
    {
        private TapLoginImpl()
        {
            EngineBridge.GetInstance()
                .Register(TapLoginConstants.TAP_LOGIN_SERVICE_CLZ, TapLoginConstants.TAP_LOGIN_SERVICE_IMPL);
        }

        private static volatile TapLoginImpl _sInstance;

        private static readonly object Locker = new object();

        public static TapLoginImpl GetInstance()
        {
            lock (Locker)
            {
                if (_sInstance == null)
                {
                    _sInstance = new TapLoginImpl();
                }
            }

            return _sInstance;
        }


        public void GetProfile(Action<Profile> action)
        {
            var command = new Command.Builder()
                .Service(TapLoginConstants.TAP_LOGIN_SERVICE)
                .Method("currentProfile")
                .Callback(true)
                .OnceTime(true)
                .CommandBuilder();

            EngineBridge.GetInstance().CallHandler(command, result =>
            {
                if (result.code != Result.RESULT_SUCCESS)
                {
                    action(null);
                    return;
                }

                if (string.IsNullOrEmpty(result.content))
                {
                    action(null);
                    return;
                }

                action(new Profile(result.content));
            });
        }

        public void GetAccessToken(Action<TapLoginToken> action)
        {
            var command = new Command.Builder()
                .Service(TapLoginConstants.TAP_LOGIN_SERVICE)
                .Method("currentAccessToken")
                .Callback(true)
                .OnceTime(true)
                .CommandBuilder();

            EngineBridge.GetInstance().CallHandler(command, result =>
            {
                if (result.code != Result.RESULT_SUCCESS)
                {
                    action(null);
                    return;
                }

                if (string.IsNullOrEmpty(result.content))
                {
                    action(null);
                    return;
                }

                action(new TapLoginToken(result.content));
            });
        }
    }
}