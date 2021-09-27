using NUnit.Framework;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using LeanCloud;
using LeanCloud.Storage;
using TapTap.Bootstrap;
using LC.Newtonsoft.Json;

namespace TapTap.Friends.Test {
    public class Friends {
        private const string RICH_PRESENCE_DISPLAY_KEY = "display";
        private const string RICH_PRESENCE_LEADERBOARD_KEY = "leadboard";
        private const string RICH_PRESENCE_RANK_KEY = "rank";

        [SetUp]
        public void Setup() {
            LCApplication.Initialize("0RiAlMny7jiz086FaU",
                "8V8wemqkpkxmAN7qKhvlh6v0pXc8JJzEZe3JFUnU",
                "https://0rialmny.cloud.tds1.tapapis.cn");
            LCObject.RegisterSubclass("_User", () => new TDSUser());
            LCLogger.LogDelegate = Log;
        }

        [TearDown]
        public void TearDown() {
            LCLogger.LogDelegate = null;
        }

        [Test]
        [Order(0)]
        [Timeout(10 * 1000)]
        public async Task Friendship() {
            TaskCompletionSource<object> tcs1 = new TaskCompletionSource<object>();
            TaskCompletionSource<object> tcs2 = new TaskCompletionSource<object>();
            TaskCompletionSource<object> tcs3 = new TaskCompletionSource<object>();
            TaskCompletionSource<object> tcs4 = new TaskCompletionSource<object>();

            TDSUser anoymous1 = await TDSUser.LoginAnonymously();
            TDSUser anoymous2 = await TDSUser.LoginAnonymously();
            TDSUser anoymous3 = await TDSUser.LoginAnonymously();
            TDSUser anoymous4 = await TDSUser.LoginAnonymously();

            await TDSUser.BecomeWithSessionToken(anoymous3.SessionToken);
            TDSFriends.FriendStatusChangedDelegate = new TDSFriendStatusChangedDelegate {
                OnNewRequestComing = req => {
                    Assert.AreEqual(req.User.ObjectId, anoymous4.ObjectId);
                    tcs1.TrySetResult(true);
                },
                OnRequestAccepted = req => {
                    Assert.AreEqual(req.Friend.ObjectId, anoymous1.ObjectId);
                    tcs2.TrySetResult(true);
                },
                OnRequestDeclined = req => {
                    Assert.AreEqual(req.Friend.ObjectId, anoymous2.ObjectId);
                    tcs3.TrySetResult(true);
                },
                OnFriendAdd = friendInfo => {
                    Assert.AreEqual(friendInfo.User.ObjectId, anoymous1.ObjectId);
                    tcs4.TrySetResult(true);
                }
            };
            await TDSFriends.Online();

            await anoymous3.ApplyFriendship(anoymous1);
            await anoymous3.ApplyFriendship(anoymous2);

            await TDSUser.BecomeWithSessionToken(anoymous4.SessionToken);
            await anoymous4.ApplyFriendship(anoymous3);
            

            await TDSUser.BecomeWithSessionToken(anoymous1.SessionToken);
            LCQuery<LCFriendshipRequest> query1 = anoymous1.GetFriendshipRequestQuery(LCFriendshipRequest.STATUS_PENDING, false, true);
            ReadOnlyCollection<LCFriendshipRequest> reqs1 = await query1.Find();
            foreach (LCFriendshipRequest req in reqs1) {
                await anoymous1.AcceptFriendshipRequest(req);
            }

            await TDSUser.BecomeWithSessionToken(anoymous2.SessionToken);
            LCQuery<LCFriendshipRequest> query2 = anoymous2.GetFriendshipRequestQuery(LCFriendshipRequest.STATUS_PENDING, false, true);
            ReadOnlyCollection<LCFriendshipRequest> reqs2 = await query2.Find();
            foreach (LCFriendshipRequest req in reqs2) {
                await anoymous2.DeclineFriendshipRequest(req);
            }

            await Task.WhenAll(tcs1.Task, tcs2.Task, tcs3.Task, tcs4.Task);

            await TDSUser.BecomeWithSessionToken(anoymous3.SessionToken);
            await TDSFriends.Offline();
        }

        [Test]
        [Order(2)]
        [Timeout(10 * 1000)]
        public async Task CRUD() {
            TDSUser user = await TDSUser.LoginAnonymously();
            TDSUser friend = await TDSUser.LoginAnonymously();

            await TDSFriends.AddFriend(user.ObjectId);

            await TDSUser.BecomeWithSessionToken(user.SessionToken);
            LCQuery<LCFriendshipRequest> query = user.GetFriendshipRequestQuery(LCFriendshipRequest.STATUS_PENDING, false, true);
            ReadOnlyCollection<LCFriendshipRequest> reqs = await query.Find();
            foreach (LCFriendshipRequest req in reqs) {
                await user.AcceptFriendshipRequest(req);
            }

            await TDSUser.BecomeWithSessionToken(friend.SessionToken);
            await TDSFriends.SetRichPresences(new Dictionary<string, string> {
                { RICH_PRESENCE_DISPLAY_KEY, "#idle" },
                { RICH_PRESENCE_LEADERBOARD_KEY, "#rank" },
                { RICH_PRESENCE_RANK_KEY, "10" },
            });

            await TDSUser.BecomeWithSessionToken(user.SessionToken);
            ReadOnlyCollection<TDSFriendInfo> friendInfos = await TDSFriends.QueryFriendList();
            foreach (TDSFriendInfo friendInfo in friendInfos) {
                Assert.AreEqual(friendInfo.RichPresence[RICH_PRESENCE_DISPLAY_KEY], "在线");
            }

            await TDSUser.BecomeWithSessionToken(friend.SessionToken);
            await TDSFriends.SetRichPresence(RICH_PRESENCE_DISPLAY_KEY, "#playing");

            await TDSUser.BecomeWithSessionToken(user.SessionToken);
            friendInfos = await TDSFriends.QueryFriendList();
            foreach (TDSFriendInfo friendInfo in friendInfos) {
                Assert.AreEqual(friendInfo.RichPresence[RICH_PRESENCE_DISPLAY_KEY], "游戏中");
            }

            await TDSUser.BecomeWithSessionToken(friend.SessionToken);
            await TDSFriends.ClearRichPresence(RICH_PRESENCE_DISPLAY_KEY);
            await TDSFriends.ClearRichPresences(new string[] { RICH_PRESENCE_RANK_KEY, RICH_PRESENCE_LEADERBOARD_KEY });

            await TDSUser.BecomeWithSessionToken(user.SessionToken);
            friendInfos = await TDSFriends.QueryFriendList();
            foreach (TDSFriendInfo friendInfo in friendInfos) {
                Assert.IsFalse(friendInfo.RichPresence.ContainsKey(RICH_PRESENCE_DISPLAY_KEY));
                Assert.IsFalse(friendInfo.RichPresence.ContainsKey(RICH_PRESENCE_RANK_KEY));
                Assert.IsFalse(friendInfo.RichPresence.ContainsKey(RICH_PRESENCE_LEADERBOARD_KEY));
            }
        }

        [Test]
        [Order(3)]
        [Timeout(10 * 1000)]
        public async Task RealtimeRichPresence() {
            TaskCompletionSource<object> tcs1 = new TaskCompletionSource<object>();
            TaskCompletionSource<object> tcs2 = new TaskCompletionSource<object>();

            TDSUser anoymous1 = await TDSUser.LoginAnonymously();
            TDSUser anoymous2 = await TDSUser.LoginAnonymously();
            TDSUser anoymous3 = await TDSUser.LoginAnonymously();

            await TDSUser.BecomeWithSessionToken(anoymous3.SessionToken);
            await TDSFriends.Online();
            TDSFriends.FriendStatusChangedDelegate = new TDSFriendStatusChangedDelegate {
                OnRichPresenceChanged = (userId, richPresence) => {
                    TestContext.WriteLine($"{userId}'s rich presence changed.");
                    TestContext.WriteLine(JsonConvert.SerializeObject(richPresence));
                    if (richPresence.TryGetValue("display", out string displayObj) &&
                        displayObj is string display) {
                        if (display == "在线") {
                            tcs1.TrySetResult(true);
                        } else if (display == "游戏中") {
                            tcs2.TrySetResult(true);
                        }
                    }
                }
            };
            await TDSFriends.AddFriend(anoymous1.ObjectId);
            await TDSFriends.AddFriend(anoymous2.ObjectId);

            await TDSUser.BecomeWithSessionToken(anoymous1.SessionToken);
            LCQuery<LCFriendshipRequest> query1 = anoymous1.GetFriendshipRequestQuery(LCFriendshipRequest.STATUS_PENDING, false, true);
            ReadOnlyCollection<LCFriendshipRequest> reqs1 = await query1.Find();
            foreach (LCFriendshipRequest req in reqs1) {
                await anoymous1.AcceptFriendshipRequest(req);
            }
            await TDSFriends.SetRichPresence(RICH_PRESENCE_DISPLAY_KEY, "#idle");
            // 查看
            ReadOnlyCollection<TDSFriendInfo> friendInfos = await TDSFriends.QueryFriendList();
            Assert.Greater(friendInfos.Count, 0);
            foreach (TDSFriendInfo friendInfo in friendInfos) {
                if (friendInfo.User.ObjectId == anoymous3.ObjectId) {
                    Assert.IsTrue(friendInfo.Online);
                }
            }

            await TDSUser.BecomeWithSessionToken(anoymous2.SessionToken);
            LCQuery<LCFriendshipRequest> query2 = anoymous2.GetFriendshipRequestQuery(LCFriendshipRequest.STATUS_PENDING, false, true);
            ReadOnlyCollection<LCFriendshipRequest> reqs2 = await query2.Find();
            foreach (LCFriendshipRequest req in reqs2) {
                await anoymous2.AcceptFriendshipRequest(req);
            }
            await TDSFriends.SetRichPresence(RICH_PRESENCE_DISPLAY_KEY, "#playing");

            await Task.WhenAll(tcs1.Task, tcs2.Task);
        }

        [Test]
        [Order(31)]
        public async Task Search() {
            TDSUser anoymous1 = await TDSUser.LoginAnonymously();
            await TDSUser.LoginAnonymously();

            ReadOnlyCollection<TDSFriendInfo> friendInfos = await TDSFriends.SearchUserByName(anoymous1.Username);
            Assert.AreEqual(friendInfos.Count, 1);
            TDSFriendInfo friendInfo = friendInfos.First();
            Assert.AreEqual(friendInfo.User.ObjectId, anoymous1.ObjectId);
        }

        [Test]
        [Order(30)]
        public async Task GenerateFriendInvitationLink() {
            TDSFriends.SetShareLink("https://friend-share.cn-e1.leanapp.cn");
            await TDSUser.LoginAnonymously();
            string url = await TDSFriends.GenerateFriendInvitationLink();
            TestContext.WriteLine($"invitation url: {url}");
        }

        // 需要执行 FriendOnlineSample 程序监听
        //[Test]
        //[Order(100)]
        //[Timeout(10 * 60 * 1000)]
        //public async Task Online() {
        //    string friendId = "6151b35e5847741659142515";

        //    await TDSUser.LoginAnonymously();

        //    await TDSFriends.AddFriend(friendId);

        //    await Task.Delay(1000);
        //    await TDSFriends.Online();

        //    await Task.Delay(3000);
        //    await TDSFriends.Offline();
        //}

        private static void Log(LCLogLevel level, string info) {
            switch (level) {
                case LCLogLevel.Debug:
                    TestContext.WriteLine($"[DEBUG] {info}\n");
                    break;
                case LCLogLevel.Warn:
                    TestContext.WriteLine($"[WARNING] {info}\n");
                    break;
                case LCLogLevel.Error:
                    TestContext.WriteLine($"[ERROR] {info}\n");
                    break;
                default:
                    TestContext.WriteLine(info);
                    break;
            }
        }
    }
}
