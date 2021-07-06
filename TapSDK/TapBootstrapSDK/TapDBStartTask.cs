using TapTap.Common;

namespace TapTap.Bootstrap
{
    public class TapDBStartTask : IStartTask
    {
        private const string ServiceName = "TDSTapDBService";

        private const string ServiceClz = "com.tds.tapdb.wrapper.TapDBService";

        private const string ServiceImpl = "com.tds.tapdb.wrapper.TapDBServiceImpl";

        private const string InitMethod = "init";

        public TapDBStartTask()
        {
            EngineBridge.GetInstance().Register(ServiceClz, ServiceImpl);
        }

        public void Invoke(TapConfig config)
        {
            if (config.DBConfig == null)
            {
                return;
            }

            if (!config.DBConfig.Enable)
            {
                return;
            }

            var command = new Command.Builder()
                .Service(ServiceName)
                .Method(InitMethod)
                .Args("clientId", config.ClientID)
                .Args("isCN", config.RegionType == RegionType.CN)
                .Args("channel", config.DBConfig.Channel)
                .Args("gameVersion", config.DBConfig.GameVersion)
                .CommandBuilder();
            // TapDB 初始化
            EngineBridge.GetInstance().CallHandler(command);

            if (!Platform.IsIOS()) return;
            
            var idfaCommand = new Command.Builder()
                .Service(ServiceName)
                .Method("advertiserIDCollectionEnabled")
                .Args("advertiserIDCollectionEnabled", config.DBConfig.AdvertiserIDCollectionEnabled == true ? 1 : 0)
                .CommandBuilder();

            // 触发 TapDB IDFA 开关
            EngineBridge.GetInstance().CallHandler(idfaCommand);
        }
    }
}