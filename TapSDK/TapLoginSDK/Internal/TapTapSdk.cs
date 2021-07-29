
namespace TapTap.Login.Internal
{
    internal static class TapTapSdk
    {
        public static readonly string VERSION = "1.0.1";

        public static string ClientId { get; private set; }
        
        public static void SdkInitialize(string clientId)
        {
            TapTapSdk.ClientId = clientId;
        }

	}
}