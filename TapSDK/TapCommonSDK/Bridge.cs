using System;

namespace TapTap.Common
{
    public class EngineBridge
    {
        private volatile static EngineBridge sInstance;

        private IBridge bridge;

        private static readonly object locker = new object();

        public static EngineBridge GetInstance()
        {
            lock (locker)
            {
                if (sInstance == null)
                {
                    sInstance = new EngineBridge();
                }
            }
            return sInstance;
        }

        private EngineBridge()
        {
            if (Platform.IsAndroid())
            {
                bridge = BridgeAndroid.GetInstance();
            }
            else if (Platform.IsIOS())
            {
                bridge = BridgeIOS.GetInstance();
            }
        }

        public void Register(string serviceClzName, string serviceImplName)
        {
            bridge?.Register(serviceClzName, serviceImplName);
        }

        public void CallHandler(Command command)
        {
            bridge?.Call(command);
        }

        public void CallHandler(Command command, Action<Result> action)
        {
            bridge?.Call(command, action);
        }


    }

}