using TapTap.Common;

namespace TapTap.Bootstrap
{
    public class TapMomentStartTask : IStartTask
    {
        private const string ServiceName = "TapMomentService";

        private const string ServiceClz = "com.tapsdk.moment.wrapper.TapMomentService";

        private const string ServiceImpl = "com.tapsdk.moment.wrapper.TapMomentServiceImpl";

        private const string InitMethod = "initWithRegion";

        public TapMomentStartTask()
        {
            EngineBridge.GetInstance().Register(ServiceClz, ServiceImpl);
        }

        public void Invoke(TapConfig config)
        {
            var command = new Command.Builder()
                .Service(ServiceName)
                .Method(InitMethod)
                .Args("clientId", config.ClientID)
                .Args("regionType", config.RegionType == RegionType.CN)
                .CommandBuilder();
            EngineBridge.GetInstance().CallHandler(command);
        }
    }
}