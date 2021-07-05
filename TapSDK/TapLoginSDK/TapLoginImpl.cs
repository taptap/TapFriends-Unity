using System.Threading.Tasks;
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

        public void ChangeConfig(bool roundCorner, bool isPortrait)
        {
            EngineBridge.GetInstance().CallHandler(new Command.Builder()
                .Service(TapLoginConstants.TAP_LOGIN_SERVICE)
                .Method("changeConfig")
                .Args("roundCorner", roundCorner)
                .Args("isPortrait", isPortrait)
                .CommandBuilder());
        }


        public async Task<Profile> FetchProfile()
        {
            var command = new Command.Builder()
                .Service(TapLoginConstants.TAP_LOGIN_SERVICE)
                .Method("fetchProfileForCurrentAccessToken")
                .Callback(true)
                .OnceTime(true)
                .CommandBuilder();
            var result = await EngineBridge.GetInstance().Emit(command);
            if (!EngineBridge.CheckResult(result))
            {
                throw new TapException((int) TapErrorCode.ERROR_CODE_BRIDGE_EXECUTE, "TapSDK fetchProfile Failed!");
            }

            var loginWrapper = new TapLoginWrapper(result.content);
            if (loginWrapper.LoginCallbackCode == 0)
            {
                return new Profile(loginWrapper.Wrapper);
            }

            throw new TapException((int) TapErrorCode.ERROR_CODE_BRIDGE_EXECUTE, loginWrapper.Wrapper);
        }

        public async Task<Profile> GetProfile()
        {
            var command = new Command.Builder()
                .Service(TapLoginConstants.TAP_LOGIN_SERVICE)
                .Method("currentProfile")
                .Callback(true)
                .OnceTime(true)
                .CommandBuilder();
            var result = await EngineBridge.GetInstance().Emit(command);
            if (!EngineBridge.CheckResult(result))
            {
                throw new TapException((int) TapErrorCode.ERROR_CODE_BRIDGE_EXECUTE, "TapSDK GetProfile Failed!");
            }

            return new Profile(result.content);
        }

        public async Task<AccessToken> GetAccessToken()
        {
            var command = new Command.Builder()
                .Service(TapLoginConstants.TAP_LOGIN_SERVICE)
                .Method("currentAccessToken")
                .Callback(true)
                .OnceTime(true)
                .CommandBuilder();

            var result = await EngineBridge.GetInstance().Emit(command);

            if (!EngineBridge.CheckResult(result))
            {
                throw new TapException((int) TapErrorCode.ERROR_CODE_BRIDGE_EXECUTE, "TapSDK GetAccessToken Failed!");
            }

            return new AccessToken(result.content);
        }

        public async Task<AccessToken> Login()
        {
            var tcs = new TaskCompletionSource<AccessToken>();
            RegisterLoginCallback(tcs);
            StartLogin();
            return await tcs.Task;
        }

        private static void StartLogin()
        {
            var command = new Command.Builder()
                .Service(TapLoginConstants.TAP_LOGIN_SERVICE)
                .Method("startTapLogin")
                .Args("permissions", new[] {"public_profile"})
                .CommandBuilder();
            EngineBridge.GetInstance().CallHandler(command);
        }

        private static void RegisterLoginCallback(TaskCompletionSource<AccessToken> tcs)
        {
            var command = new Command.Builder()
                .Service(TapLoginConstants.TAP_LOGIN_SERVICE)
                .Method("registerLoginCallback")
                .Callback(true)
                .OnceTime(false)
                .CommandBuilder();

            EngineBridge.GetInstance().CallHandler(command, result =>
            {
                if (!EngineBridge.CheckResult(result))
                {
                    tcs.TrySetException(new TapException((int) TapErrorCode.ERROR_CODE_BRIDGE_EXECUTE,
                        "TapSDK Login Failed!"));
                    return;
                }

                var wrapper = new TapLoginWrapper(result.content);
                
                switch (wrapper.LoginCallbackCode)
                {
                    case 0:
                        tcs.TrySetResult(new AccessToken(wrapper.Wrapper));
                        break;
                    case 1:
                        tcs.TrySetCanceled();
                        break;
                    default:
                        var tapError = TapError.SafeConstructorTapError(wrapper.Wrapper);
                        tcs.TrySetException(new TapException(tapError.code,
                            tapError.errorDescription));
                        break;
                }
                UnRegisterLoginCallback();
            });
        }

        private static void UnRegisterLoginCallback()
        {
            var command = new Command.Builder()
                .Service(TapLoginConstants.TAP_LOGIN_SERVICE)
                .Method("unregisterLoginCallback")
                .CommandBuilder();
            EngineBridge.GetInstance().CallHandler(command);
        }
    }
}