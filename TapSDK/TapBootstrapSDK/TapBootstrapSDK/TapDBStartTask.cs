using TapTap.Common;

namespace TapTap.Bootstrap
{
    public class TapDBStartTask : IStartTask
    {
        private const string ServiceName = "TDSTapDBService";

        private const string ServiceClz = "com.tds.tapdb.wrapper.TapDBService";

        private const string ServiceImpl = "com.tds.tapdb.wrapper.TapDBServiceImpl";

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
            
            // TapDB 初始化
            var command = new Command.Builder()
                .Service(ServiceName)
                .Method("init")
                .Args("clientId", config.ClientID)
                .Args("channel", config.DBConfig.Channel)
                .Args("isCN", config.RegionType == RegionType.CN)
                .Args("gameVersion", config.DBConfig.GameVersion).CommandBuilder();
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