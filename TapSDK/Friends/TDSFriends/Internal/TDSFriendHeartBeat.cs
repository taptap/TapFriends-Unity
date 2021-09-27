using System;
using System.Threading.Tasks;
using LeanCloud.Realtime.Internal.Connection;

namespace TapTap.Friends.Internal {
    internal class TDSFriendHeartBeat : LCHeartBeat {
        private readonly TDSFriendConnection connection;

        internal TDSFriendHeartBeat(TDSFriendConnection connection, Action onTimeout)
            : base(onTimeout) {
            this.connection = connection;
        }

        protected override async Task SendPing() {
            try {
                await connection.SendText("{}");
            } catch (Exception e) {
                // TODO 输出错误日志

            }
        }
    }
}
