using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using LeanCloud;
using LeanCloud.Common;
using LeanCloud.Storage;
using LeanCloud.Storage.Internal.Object;
using TapTap.Bootstrap;
using TapTap.Friends.Internal;
using LC.Newtonsoft.Json;

namespace TapTap.Friends {
    internal class TDSRichPresence {
        internal bool Online { get; set; }
        internal Dictionary<string, object> RichPresence { get; set; }
    }

    public class TDSFriends {
        private const string NOTIFICATION_APPLY = "friendship/request/apply";
        private const string NOTIFICATION_ACCEPT = "friendship/request/accept";
        private const string NOTIFICATION_DECLINE = "friendship/request/refund";
        private const string NOTIFICATION_ONLINE = "friendship/friend/online";
        private const string NOTIFICATION_RICH_PRESENCE_CHANGED = "friendship/rich_presence/update";

        private const string USER_ID_KEY = "userId";
        private const string REQUEST_ID_KEY = "requestId";
        private const string ONLINE_KEY = "online";

        public static TDSFriendStatusChangedDelegate FriendStatusChangedDelegate { get; set; }

        private static TDSFriendConnection connection;

        private static string shareLink;

        public static async Task Online() {
            bool needWebSocket = await NeedWebSocket();
            if (!needWebSocket) {
                return;
            }
            connection = new TDSFriendConnection();
            connection.OnNotification = notification => {
                if (notification.TryGetValue("header", out object headerObj) &&
                    headerObj is Dictionary<string, object> header &&
                    header.TryGetValue("path", out object path) &&
                    notification.TryGetValue("body", out object dataObj) &&
                    dataObj is Dictionary<string, object> data) {
                    switch (path) {
                        case NOTIFICATION_APPLY:
                            OnApplyNotification(data);
                            break;
                        case NOTIFICATION_ACCEPT:
                            OnAcceptNotication(data);
                            break;
                        case NOTIFICATION_DECLINE:
                            OnDeclineNotification(data);
                            break;
                        case NOTIFICATION_ONLINE:
                            OnFriendOnlineNotification(data);
                            break;
                        case NOTIFICATION_RICH_PRESENCE_CHANGED:
                            OnFriendRichPresenceChangedNotification(data);
                            break;
                        default:
                            break;
                    }
                }
            };
            await connection.Connect();
        }

        public static async Task Offline() {
            FriendStatusChangedDelegate = null;
            await connection.Close();
            connection = null;
        }

        public static async Task AddFriend(string userId, Dictionary<string, object> attrs = null) {
            if (string.IsNullOrEmpty(userId)) {
                throw new ArgumentNullException(nameof(userId));
            }
            TDSUser current = await TDSUser.GetCurrent();
            if (current == null) {
                throw new Exception("Not logged in.");
            }
            await current.ApplyFriendship(userId, attrs);
        }

        public static async Task<ReadOnlyCollection<LCFriendshipRequest>> QueryFriendRequestList(int status, int from, int limit) {
            TDSUser current = await TDSUser.GetCurrent();
            if (current == null) {
                throw new Exception("Not logged in.");
            }
            LCQuery<LCFriendshipRequest> query = current.GetFriendshipRequestQuery(status, false, true)
                .Skip(from)
                .Limit(limit);
            return await query.Find();
        }

        public static async Task AcceptFriendshipRequest(LCFriendshipRequest request) {
            TDSUser current = await TDSUser.GetCurrent();
            if (current == null) {
                throw new Exception("Not logged in.");
            }
            await current.AcceptFriendshipRequest(request);
        }

        public static async Task DeclineFriendshipRequest(LCFriendshipRequest request) {
            TDSUser current = await TDSUser.GetCurrent();
            if (current == null) {
                throw new Exception("Not logged in.");
            }
            await current.DeclineFriendshipRequest(request);
        }

        public static async Task DeleteFriendshipRequest(LCFriendshipRequest request) {
            TDSUser current = await TDSUser.GetCurrent();
            if (current == null) {
                throw new Exception("Not logged in.");
            }
            await current.DeleteFriendshipRequest(request);
        }

        public static async Task DeleteFriend(string targetId) {
            if (string.IsNullOrEmpty(targetId)) {
                throw new ArgumentNullException(nameof(targetId));
            }
            TDSUser current = await TDSUser.GetCurrent();
            if (current == null) {
                throw new Exception("Not logged in.");
            }
            await current.Unfollow(targetId);
        }

        public static async Task<ReadOnlyCollection<TDSFriendInfo>> QueryFriendList() {
            TDSUser current = await TDSUser.GetCurrent();
            if (current == null) {
                throw new Exception("Not logged in.");
            }

            LCQuery<LCObject> query = current.GetFriendshipQuery();
            ReadOnlyCollection<LCObject> friendships = await query.Find();
            List<TDSUser> friends = friendships.Select(friendship => friendship["followee"] as TDSUser).ToList();
            if (friends == null || friends.Count == 0) {
                return null;
            }

            return await QueryFriendInfos(friends);
        }

        public static async Task<bool> CheckFriendship(string userId) {
            TDSUser current = await TDSUser.GetCurrent();
            if (current == null) {
                throw new Exception("Not logged in.");
            }

            LCQuery<LCObject> query = current.GetFriendshipQuery();
            query.WhereEqualTo("followee", TDSUser.CreateWithoutData(TDSUser.CLASS_NAME, userId));
            return await query.Count() > 0;
        }

        public static async Task<ReadOnlyCollection<TDSFriendInfo>> SearchUserByName(string username) {
            string path = "usersInternalFind";
            Dictionary<string, object> queryParams = new Dictionary<string, object> {
                { "username", username }
            };
            Dictionary<string, object> response = await LCCore.HttpClient.Get<Dictionary<string, object>>(path,
                queryParams: queryParams);
            if (response.TryGetValue("results", out object resultsObj) &&
                resultsObj is List<object> results) {
                List<TDSUser> users = results.Select(data => {
                    LCObjectData objectData = LCObjectData.Decode(data as Dictionary<string, object>);
                    TDSUser user = new TDSUser();
                    user.Merge(objectData);
                    return user;
                }).ToList();
                return await QueryFriendInfos(users);
            }
            return null;
        }

        private static async Task<ReadOnlyCollection<TDSFriendInfo>> QueryFriendInfos(List<TDSUser> users) {
            if (users == null || users.Count == 0) {
                return null;
            }

            IEnumerable<string> ids = users.Select(user => user.ObjectId);
            List<TDSFriendInfo> friendInfos = new List<TDSFriendInfo>(ids.Count());
            ReadOnlyCollection<TDSRichPresence> richPresences = await QueryRichPresences(ids);
            if (richPresences != null &&
                users.Count == richPresences.Count) {
                for (int i = 0; i < users.Count; i++) {
                    TDSUser user = users[i];
                    TDSRichPresence presence = richPresences[i];
                    TDSFriendInfo friendInfo = new TDSFriendInfo {
                        User = user,
                        Online = presence.Online,
                        RichPresence = presence.RichPresence.ToDictionary(kv => kv.Key, kv => kv.Value as string)
                    };
                    friendInfos.Add(friendInfo);
                }
            }
            return friendInfos.AsReadOnly();
        }

        public static Task SetRichPresence(string key, string value) {
            if (string.IsNullOrEmpty(key)) {
                throw new ArgumentNullException(nameof(key));
            }
            if (value == null) {
                throw new ArgumentNullException(nameof(value));
            }

            string path = "friend/v1/rich-presence/users/me";
            Dictionary<string, object> data = new Dictionary<string, object> {
                { "config", new List<object> {
                    new Dictionary<string, object> {
                        { "key", key },
                        { "value", value }
                    }
                } }
            };
            return LCCore.HttpClient.Post<Dictionary<string, object>>(path,
                data: data,
                withAPIVersion: false);
        }

        public static Task ClearRichPresence(string key) {
            return SetRichPresence(key, string.Empty);
        }

        public static Task SetRichPresences(Dictionary<string, string> richPresences) {
            if (richPresences == null || richPresences.Count == 0) {
                throw new ArgumentNullException(nameof(richPresences));
            }

            string path = "friend/v1/rich-presence/users/me";
            List<object> presences = richPresences.Select(kv => new Dictionary<string, string> {
                { "key", kv.Key },
                { "value", kv.Value }
            }).ToList<object>();
            Dictionary<string, object> data = new Dictionary<string, object> {
                { "config", presences }
            };
            return LCCore.HttpClient.Post<Dictionary<string, object>>(path,
                data: data,
                withAPIVersion: false);
        }

        public static Task ClearRichPresences(IEnumerable<string> keys) {
            if (keys == null || keys.Count() == 0) {
                throw new ArgumentNullException(nameof(keys));
            }

            Dictionary<string, string> richPresences = keys.ToDictionary(k => k, k => string.Empty);
            return SetRichPresences(richPresences);
        }

        public static void SetShareLink(string url) {
            shareLink = url;
        }

        public static async Task<string> GenerateFriendInvitationLink() {
            if (string.IsNullOrEmpty(shareLink)) {
                throw new ArgumentNullException(nameof(shareLink));
            }

            TDSUser current = await TDSUser.GetCurrent();
            if (current == null) {
                throw new Exception("Not logged in.");
            }

            return GenerateShareLink(current.Username, "invitation", current.ObjectId);
        }

        private static string GenerateShareLink(string roleName, string funcName, string id) {
            Dictionary<string, object> ext = new Dictionary<string, object> {
                { "func", funcName },
                { "identity", id }
            };
            Dictionary<string, object> ps = new Dictionary<string, object> {
                { "role_name", roleName },
                { "ext", Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(ext))) }
            };

            return $"{shareLink}?p={Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(ps)))}";
        }

        private static async void OnApplyNotification(Dictionary<string, object> data) {
            LCFriendshipRequest request = await GetFriendshipRequest(data);
            if (request != null) {
                FriendStatusChangedDelegate?.OnNewRequestComing?.Invoke(request);
            }
        }

        private static async void OnAcceptNotication(Dictionary<string, object> data) {
            LCFriendshipRequest request = await GetFriendshipRequest(data);
            if (request != null) {
                FriendStatusChangedDelegate?.OnRequestAccepted?.Invoke(request);
            }

            ReadOnlyCollection<TDSFriendInfo> friendInfos = await QueryFriendInfos(new List<TDSUser> { request.Friend as TDSUser });
            if (friendInfos == null || friendInfos.Count == 0) {
                return;
            }
            FriendStatusChangedDelegate?.OnFriendAdd?.Invoke(friendInfos.First());
        }

        private static async  void OnDeclineNotification(Dictionary<string, object> data) {
            LCFriendshipRequest request = await GetFriendshipRequest(data);
            if (request != null) {
                FriendStatusChangedDelegate?.OnRequestDeclined?.Invoke(request);
            }
        }

        private static async Task<LCFriendshipRequest> GetFriendshipRequest(Dictionary<string, object> data) {
            if (data.TryGetValue(REQUEST_ID_KEY, out object requestIdObj) &&
                requestIdObj is string requestId) {
                LCQuery<LCFriendshipRequest> query = LCFriendshipRequest.GetQuery();
                try {
                    LCFriendshipRequest request = await query.Get(requestId);
                    return request;
                } catch (Exception e) {
                    LCLogger.Error(e);
                }
            }
            return null;
        }

        private static void OnFriendOnlineNotification(Dictionary<string, object> data) {
            if (data.TryGetValue(ONLINE_KEY, out object onlineObj) &&
                data.TryGetValue(USER_ID_KEY, out object userIdObj) &&
                userIdObj is string userId) {
                bool online = (bool)onlineObj;
                if (online) {
                    FriendStatusChangedDelegate?.OnFriendOnline?.Invoke(userId);
                } else {
                    FriendStatusChangedDelegate?.OnFriendOffline?.Invoke(userId);
                }
            }
        }

        private static async void OnFriendRichPresenceChangedNotification(Dictionary<string, object> data) {
            if (data.TryGetValue(USER_ID_KEY, out object userIdObj) &&
                userIdObj is string userId) {
                LCQuery<TDSUser> query = TDSUser.GetQuery();
                query.Include("richPresence");
                TDSUser user = await query.Get(userId);
                if (user == null) {
                    return;
                }

                ReadOnlyCollection<TDSFriendInfo> friendInfos = await QueryFriendInfos(new List<TDSUser> { user });
                if (friendInfos == null || friendInfos.Count == 0) {
                    return;
                }

                FriendStatusChangedDelegate?.OnRichPresenceChanged?.Invoke(userId, friendInfos.First().RichPresence);
            }
        }


        private static async Task<bool> NeedWebSocket() {
            string path = "friend/v1/rich-presence/config";
            Dictionary<string, object> response = await LCCore.HttpClient.Get<Dictionary<string, object>>(path,
                withAPIVersion: false);
            if (response.TryGetValue("enabled", out object enabledObj) &&
                enabledObj is int enabled) {
                return enabled == 1;
            }
            return false;
        }

        internal static async Task<ReadOnlyCollection<TDSRichPresence>> QueryRichPresences(IEnumerable<string> userIds) {
            if (userIds == null || userIds.Count() == 0) {
                throw new ArgumentNullException(nameof(userIds));
            }

            string path = "friend/v1/rich-presence/users";
            Dictionary<string, object> queryParams = new Dictionary<string, object> {
                { "ids", $"{{{string.Join(",", userIds)}}}" }
            };
            Dictionary<string, object> response = await LCCore.HttpClient.Get<Dictionary<string, object>>(path,
                queryParams: queryParams,
                withAPIVersion: false);
            if (response.TryGetValue("results", out object resultsObj) &&
                resultsObj is List<object> results) {
                return results.Select(result => {
                    TDSRichPresence richPresence = new TDSRichPresence();
                    if (result is Dictionary<string, object> dict) {
                        richPresence.Online = (bool)dict["online"];
                        richPresence.RichPresence = dict["richPresence"] as Dictionary<string, object>;
                    }
                    return richPresence;
                }).ToList().AsReadOnly();
            }

            return null;
        }
    }
}
