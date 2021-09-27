using LeanCloud.Common;
using LeanCloud.Realtime.Internal.Router;

namespace TapTap.Friends.Internal {
    public class TDSFriendRouter {
        public TDSFriendRouter() {
        }

        public LCRTMServer GetServer() {
            string url = $"wss://{LCCore.AppId.Substring(0, 8)}.ws.tds1.tapapis.cn/ws/leancloud/v1";
            return new LCRTMServer {
                Primary = url,
                Secondary = url
            };
        }
    }
}
