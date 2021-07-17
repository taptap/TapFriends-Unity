using TapTap.Common;

namespace TapTap.Bootstrap
{
    public class TapBootstrapImpl : ITapBootstrap
    {
        private readonly TapStartTaskHolder _taskHolder;

        private TapBootstrapImpl()
        {
            _taskHolder = new TapStartTaskHolder();
            
            _taskHolder.AddTask(new TapTapLoginStartTask());
            _taskHolder.AddTask(new TapMomentStartTask());
            _taskHolder.AddTask(new TapStorageStartTask());
        }

        private static volatile TapBootstrapImpl _sInstance;

        private static readonly object Locker = new object();

        public static TapBootstrapImpl GetInstance()
        {
            lock (Locker)
            {
                if (_sInstance == null)
                {
                    _sInstance = new TapBootstrapImpl();
                }
            }

            return _sInstance;
        }

        public void Init(TapConfig config)
        {
            TapCommon.SetXua();
            _taskHolder.Invoke(config);
        }
    }
}