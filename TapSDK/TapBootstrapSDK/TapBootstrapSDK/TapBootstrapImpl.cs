using System.Reflection;
using System.Threading.Tasks;
using LeanCloud.Storage;
using TapTap.Common;
using UnityEngine;

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
            _taskHolder.AddTask(new TapDBStartTask());
            _taskHolder.AddTask(new TapAchievementStartTask());
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

        public async void Init(TapConfig config)
        {
            _taskHolder.Invoke(config);

            TapCommon.SetXua();
            
            await TDSUser.GetCurrent();
            
            TapCommon.RegisterProperties("sessionToken", new SessionTokenProperty());

            TapCommon.RegisterProperties("objectId", new ObjectIdProperty());
            
        }

        private class SessionTokenProperty : ITapPropertiesProxy
        {
            public string GetProperties()
            {
                Debug.Log($"sessionToken User:{GetCurrentUser()}");
                var sessionToken = GetCurrentUser()?.SessionToken;
                return string.IsNullOrEmpty(sessionToken) ? "" : sessionToken;
            }
        }

        private class ObjectIdProperty : ITapPropertiesProxy
        {
            public string GetProperties()
            {
                Debug.Log($"objectId User:{GetCurrentUser()}");
                var objectId = GetCurrentUser()?.ObjectId;
                return string.IsNullOrEmpty(objectId) ? "" : objectId;
            }
        }

        private static LCUser GetCurrentUser()
        {
            var field = typeof(LCUser).GetField("currentUser", BindingFlags.Static | BindingFlags.NonPublic);
            return field?.GetValue(new LCUser()) as LCUser;
        }
    }
}