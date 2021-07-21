using TapTap.Common;

namespace TapTap.Bootstrap
{
    public class TapAchievementStartTask : IStartTask
    {
        private const string ServiceName = "TDSAchievementService";

        private const string ServiceClz = "com.tds.achievement.wrapper.TDSAchievementService";

        private const string ServiceImpl = "com.tds.achievement.wrapper.TDSAchievementServiceImpl";

        private const string InitMethod = "init";

        public TapAchievementStartTask()
        {
            EngineBridge.GetInstance().Register(ServiceClz, ServiceImpl);
        }

        public void Invoke(TapConfig config)
        {
            var command = new Command.Builder()
                .Service(ServiceName)
                .Method(InitMethod)
                .Args("config", config.ToJson())
                .CommandBuilder();
            EngineBridge.GetInstance().CallHandler(command);
        }
    }
}