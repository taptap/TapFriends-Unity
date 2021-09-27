using System;
using System.Collections.Generic;
using LeanCloud.Storage;

namespace TapTap.Friends {
    public class TDSFriendStatusChangedDelegate {
        public Action<LCFriendshipRequest> OnNewRequestComing { get; set; }
        public Action<LCFriendshipRequest> OnRequestAccepted { get; set; }
        public Action<LCFriendshipRequest> OnRequestDeclined { get; set; }

        public Action<TDSFriendInfo> OnFriendAdd { get; set; }

        public Action<string> OnFriendOnline { get; set; }
        public Action<string> OnFriendOffline { get; set; }

        public Action<string, Dictionary<string, string>> OnRichPresenceChanged { get; set; }
    }
}
