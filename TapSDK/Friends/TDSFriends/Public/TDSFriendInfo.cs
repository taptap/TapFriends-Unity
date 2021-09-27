using System.Collections.Generic;
using TapTap.Bootstrap;

namespace TapTap.Friends {
    public class TDSFriendInfo {
        public TDSUser User {
            get; internal set;
        }

        public bool Online {
            get; internal set;
        }

        public Dictionary<string, string> RichPresence {
            get; internal set;
        }

        public TDSFriendInfo() {
        }
    }
}
