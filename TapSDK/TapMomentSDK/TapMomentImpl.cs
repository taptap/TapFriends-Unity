using System;
using System.Collections.Generic;
using TapTap.Common;
using UnityEngine;

namespace TapTap.Moment
{
    public class MomentImpl : ITapMoment
    {
        private static readonly string CLZ_NAME = "com.tapsdk.moment.wrapper.TapMomentService";

        private static readonly string IMP_NAME = "com.tapsdk.moment.wrapper.TapMomentServiceImpl";

        private static readonly string SERVICE_NAME = "TapMomentService";

        private ScreenOrientation _requestOrientation = 0;
        private ScreenOrientation _originOrientation = 0;
        private int _autorotateToLandscapeLeft;
        private int _autorotateToLandscapeRight;
        private int _autorotateToPortrait;
        private int _autorotateToPortraitUpsideDown;
        private bool _useAutoRotate = true;
        private bool _isAppear = false;

        private int _nativeOrientation = -1;

        private MomentImpl()
        {
            EngineBridge.GetInstance().Register(CLZ_NAME, IMP_NAME);
        }

        private static volatile MomentImpl _sInstance;

        private static readonly object Locker = new object();

        public static MomentImpl GetInstance()
        {
            lock (Locker)
            {
                if (_sInstance == null)
                {
                    _sInstance = new MomentImpl();
                }
            }

            return _sInstance;
        }

        public void SetCallback(Action<int, string> callback)
        {
            var command = new Command.Builder()
                .Service(SERVICE_NAME)
                .Method("setCallback")
                .Callback(true)
                .OnceTime(false)
                .CommandBuilder();

            EngineBridge.GetInstance().CallHandler(command, (result) =>
            {
                if (result.code != Result.RESULT_SUCCESS)
                {
                    return;
                }

                if (string.IsNullOrEmpty(result.content))
                {
                    return;
                }

                var bean = new MomentCallbackBean(result.content);

                if (Platform.IsAndroid())
                {
                    if (AndroidOrientationInterceptor(bean.code))
                    {
                        callback(bean.code, bean.message);
                    }
                }
                else
                {
                    callback(bean.code, bean.message);
                }
            });
        }

        public void Init(string clientId)
        {
            EngineBridge.GetInstance().CallHandler(new Command.Builder()
                .Service(SERVICE_NAME)
                .Method("init")
                .Args("clientId", clientId)
                .CommandBuilder());
        }

        public void Init(string clientId, bool isCN)
        {
            EngineBridge.GetInstance().CallHandler(new Command.Builder()
                .Service(SERVICE_NAME)
                .Method("initWithRegion")
                .Args("clientId", clientId)
                .Args("regionType", isCN)
                .CommandBuilder());
        }

        public void Open(Orientation orientation)
        {
            InitOrientationSetting((int) orientation);
            EngineBridge.GetInstance().CallHandler(new Command.Builder()
                .Service(SERVICE_NAME)
                .Method("open")
                .Args("config", (int) orientation)
                .CommandBuilder());
        }

        public void Publish(Orientation orientation, string[] imagePaths, string content)
        {
            InitOrientationSetting((int) orientation);
            EngineBridge.GetInstance().CallHandler(new Command.Builder()
                .Service(SERVICE_NAME)
                .Method("publish")
                .Args("config", (int) orientation)
                .Args("imagePaths", imagePaths)
                .Args("content", content)
                .CommandBuilder());
        }

        public void PublishVideo(Orientation orientation, string[] videoPaths, string title, string desc)
        {
            InitOrientationSetting((int) orientation);
            EngineBridge.GetInstance().CallHandler(new Command.Builder()
                .Service(SERVICE_NAME)
                .Method("publishVideo")
                .Args("config", (int) orientation)
                .Args("videoPaths", videoPaths)
                .Args("title", title)
                .Args("desc", desc)
                .CommandBuilder());
        }

        public void PublishVideo(Orientation orientation, string[] videoPaths, string[] imagePaths, string title,
            string desc)
        {
            InitOrientationSetting((int) orientation);
            EngineBridge.GetInstance().CallHandler(new Command.Builder()
                .Service(SERVICE_NAME)
                .Method("publishVideoImage")
                .Args("config", (int) orientation)
                .Args("videoPaths", videoPaths)
                .Args("imagePaths", imagePaths)
                .Args("title", title)
                .Args("desc", desc)
                .CommandBuilder());
        }

        public void FetchNotification()
        {
            EngineBridge.GetInstance().CallHandler(new Command.Builder()
                .Service(SERVICE_NAME)
                .Method("fetchNotification")
                .CommandBuilder());
        }

        public void Close()
        {
            EngineBridge.GetInstance().CallHandler(new Command.Builder()
                .Service(SERVICE_NAME)
                .Method("close")
                .CommandBuilder());
        }

        public void Close(string title, string content)
        {
            EngineBridge.GetInstance().CallHandler(new Command.Builder()
                .Service(SERVICE_NAME)
                .Args("title", title)
                .Args("content", content)
                .Method("closeWithConfirmWindow")
                .CommandBuilder());
        }

        public void SetAndroidRequestOrientation(int orientation)
        {
            EngineBridge.GetInstance().CallHandler(new Command.Builder()
                .Service(SERVICE_NAME)
                .Args("orientation", orientation)
                .Method("setRequestOrientation")
                .CommandBuilder());
        }

        public void SetUseAutoRotate(bool useAuto)
        {
            if (Platform.IsAndroid())
            {
                _useAutoRotate = useAuto;
            }
        }

        public void DirectlyOpen(Orientation orientation, string page, Dictionary<string, object> extras)
        {
            InitOrientationSetting((int) orientation);
            EngineBridge.GetInstance().CallHandler(new Command.Builder()
                .Service(SERVICE_NAME)
                .Method("directlyOpen")
                .Args("config", (int) orientation)
                .Args("page", page)
                .Args("extras", Json.Serialize(extras))
                .CommandBuilder());
        }

        private bool AndroidOrientationInterceptor(int code)
        {
            switch (code)
            {
                case (int) CallbackCode.CALLBACK_CODE_MOMENT_APPEAR:
                    SetRequestOrientation(false);
                    _isAppear = true;
                    return true;
                case (int) CallbackCode.CALLBACK_CODE_MOMENT_DISAPPEAR:
                    SetOriginOrientation();
                    _isAppear = false;
                    return true;
                case (int) CallbackCode.CALLBACK_CODE_ON_RESUME:
                {
                    if (_isAppear)
                    {
                        SetRequestOrientation(true);
                    }

                    return false;
                }
            }

            if (code != (int) CallbackCode.CALLBACK_CODE_ON_STOP) return true;
            if (_isAppear)
            {
                SetOriginOrientation();
            }

            return false;
        }

        private void InitOrientationSetting(int config)
        {
            if (!Platform.IsAndroid())
            {
                return;
            }

            _originOrientation = Screen.orientation;
            _autorotateToPortraitUpsideDown = Screen.autorotateToPortraitUpsideDown ? 1 : 0;
            _autorotateToPortrait = Screen.autorotateToPortrait ? 1 : 0;
            _autorotateToLandscapeRight = Screen.autorotateToLandscapeRight ? 1 : 0;
            _autorotateToLandscapeLeft = Screen.autorotateToLandscapeLeft ? 1 : 0;
            GetRequestOrientation(config);
            Debug.Log("orgin orientation = " + _originOrientation);
            Debug.Log("request orientation = " + _requestOrientation);
            Debug.Log(" autoPd = " + _autorotateToPortraitUpsideDown + " autoP = " + _autorotateToPortrait +
                      " autoLl = " + _autorotateToLandscapeLeft + " autolr = " + _autorotateToLandscapeRight);
        }

        private void GetRequestOrientation(int config)
        {
            if (!Platform.IsAndroid())
            {
                return;
            }

            _nativeOrientation = config;
            switch (_nativeOrientation)
            {
                case (int) Orientation.ORIENTATION_DEFAULT:
                    _requestOrientation = ScreenOrientation.AutoRotation;
                    break;
                case (int) Orientation.ORIENTATION_LANDSCAPE:
                    _requestOrientation = ScreenOrientation.LandscapeLeft;
                    break;
                case (int) Orientation.ORIENTATION_PORTRAIT:
                    _requestOrientation = ScreenOrientation.Portrait;
                    break;
            }
        }

        private void SetRequestOrientation(bool isResume)
        {
            if (!Platform.IsAndroid())
            {
                return;
            }

            ScreenOrientation orientation = _requestOrientation;
            ScreenOrientation originOrientation = _originOrientation;
            if (orientation == ScreenOrientation.LandscapeLeft &&
                (originOrientation == ScreenOrientation.LandscapeLeft ||
                 originOrientation == ScreenOrientation.LandscapeRight))
            {
                return;
            }

            Debug.Log("APPERAR SET REQUEST ORIENTATION = " + orientation);

            if (isResume)
            {
                SetAndroidRequestOrientation(_nativeOrientation);
            }

            ScreenOrientation currentOrientation;
            if (orientation == ScreenOrientation.AutoRotation || orientation == ScreenOrientation.LandscapeLeft)
            {
                currentOrientation = GetDeviceOrientation(orientation);
            }
            else
            {
                currentOrientation = orientation;
            }

            Screen.orientation = currentOrientation;
            Debug.Log("current device currentOrientation = " + currentOrientation);

            if (!IsAutoRotate()) return;
            Screen.orientation = ScreenOrientation.AutoRotation;
            Screen.autorotateToLandscapeLeft = true;
            Screen.autorotateToLandscapeRight = true;
            Screen.autorotateToPortrait = (orientation != ScreenOrientation.LandscapeLeft);
            Screen.autorotateToPortraitUpsideDown = false;
            Debug.Log("Set Request Orientation:" + Screen.orientation);
        }

        private void SetOriginOrientation()
        {
            if (!Platform.IsAndroid())
            {
                return;
            }

            ScreenOrientation orientation = _originOrientation;
            ScreenOrientation requestOrientation = _requestOrientation;
            if (requestOrientation == ScreenOrientation.LandscapeLeft &&
                (orientation == ScreenOrientation.LandscapeLeft || orientation == ScreenOrientation.LandscapeRight))
            {
                return;
            }

            Debug.Log("设置成原始方向");
            Screen.orientation = _originOrientation;

            Screen.autorotateToLandscapeLeft = _autorotateToLandscapeLeft > 0;
            Screen.autorotateToLandscapeRight = _autorotateToLandscapeRight > 0;
            Screen.autorotateToPortrait = _autorotateToPortrait > 0;
            Screen.autorotateToPortraitUpsideDown = _autorotateToPortraitUpsideDown > 0;

            if (!IsAutoRotate()) return;
            Debug.Log("恢复自动旋转");
            Screen.orientation = ScreenOrientation.AutoRotation;
        }

        private bool IsAutoRotate()
        {
            return _autorotateToLandscapeLeft > 0 || _autorotateToLandscapeRight > 0 ||
                   _autorotateToPortrait > 0 || _autorotateToPortraitUpsideDown > 0;
        }

        private ScreenOrientation GetDeviceOrientation(ScreenOrientation orientation)
        {
            ScreenOrientation currentOrientation;
            var deviceOrientation = Input.deviceOrientation;
            switch (deviceOrientation)
            {
                case DeviceOrientation.LandscapeLeft:
                    currentOrientation = ScreenOrientation.LandscapeLeft;
                    break;
                case DeviceOrientation.LandscapeRight:
                    currentOrientation = ScreenOrientation.LandscapeRight;
                    break;
                case DeviceOrientation.Portrait when orientation != ScreenOrientation.LandscapeLeft:
                    currentOrientation = ScreenOrientation.Portrait;
                    break;
                default:
                    currentOrientation = orientation;
                    break;
            }

            return currentOrientation;
        }

        private Command ConstructorCommand(string method, Dictionary<string, object> dic, bool callbackEnable)
        {
            return new Command(SERVICE_NAME, method, callbackEnable, dic);
        }
    }
}