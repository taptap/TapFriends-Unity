namespace TapTap.Bootstrap
{
    public class TapBootstrap
    {
        public static void Init(TapConfig tapConfig)
        {
            TapBootstrapImpl.GetInstance().Init(tapConfig);
        }
    }
}