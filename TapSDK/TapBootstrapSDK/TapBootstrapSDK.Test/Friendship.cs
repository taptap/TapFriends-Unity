using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using LeanCloud;
using LeanCloud.Storage;
using TapTap.Bootstrap;

namespace TapBootstrapTest {
    public class Friendship {
        private TDSUser annoymous1;
        private TDSUser annoymous2;
        private TDSUser annoymous3;
        private TDSUser annoymous4;


        [SetUp]
        public async Task Setup() {
            LCApplication.Initialize("0RiAlMny7jiz086FaU",
                "8V8wemqkpkxmAN7qKhvlh6v0pXc8JJzEZe3JFUnU",
                "https://0rialmny.cloud.tds1.tapapis.cn");
            LCObject.RegisterSubclass("_User", () => new TDSUser());
            LCLogger.LogDelegate = Log;

            annoymous1 = await TDSUser.LoginAnonymously();
            annoymous2 = await TDSUser.LoginAnonymously();
            annoymous3 = await TDSUser.LoginAnonymously();
            annoymous4 = await TDSUser.LoginAnonymously();
        }

        [TearDown]
        public async Task TearDown() {
            LCLogger.LogDelegate = null;
            await annoymous4.UnregisterFriendshipNotification();
        }

        [Test]
        [Timeout(20000)]
        public async Task TestFriendship() {
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();

            // annoymous3
            await TDSUser.BecomeWithSessionToken(annoymous3.SessionToken);
            await annoymous3.RegisterFriendshipNotification(new TDSUser.FriendshipNotification {
                OnNewRequestComing = req => {
                    Console.WriteLine($"{req.User.ObjectId} requested you for friend.");
                    Assert.AreEqual(req.User.ObjectId, annoymous4.ObjectId);
                },
                OnRequestAccepted = req => {
                    Console.WriteLine($"{req.Friend.ObjectId} accepted your request.");
                    Assert.AreEqual(req.Friend.ObjectId, annoymous1.ObjectId);
                },
                OnRequestDeclined = req => {
                    Console.WriteLine($"{req.Friend.ObjectId} declined your request.");
                    Assert.AreEqual(req.Friend.ObjectId, annoymous2.ObjectId);

                    tcs.TrySetResult(true);
                }
            });
            await annoymous3.ApplyFriendship(annoymous1);
            await annoymous3.ApplyFriendship(annoymous2);

            // annoymous4
            await TDSUser.BecomeWithSessionToken(annoymous4.SessionToken);
            await annoymous4.ApplyFriendship(annoymous3);

            // annoymous1
            await TDSUser.BecomeWithSessionToken(annoymous1.SessionToken);
            LCQuery<LCFriendshipRequest> query1 = annoymous1.GetFriendshipRequestQuery(LCFriendshipRequest.STATUS_PENDING, false, true);
            ReadOnlyCollection<LCFriendshipRequest> reqs1 = await query1.Find();
            foreach (LCFriendshipRequest req in reqs1) {
                await annoymous1.AcceptFriendshipRequest(req);
            }

            // annoymous2
            await TDSUser.BecomeWithSessionToken(annoymous2.SessionToken);
            LCQuery<LCFriendshipRequest> query2 = annoymous2.GetFriendshipRequestQuery(LCFriendshipRequest.STATUS_PENDING, false, true);
            ReadOnlyCollection<LCFriendshipRequest> reqs2 = await query2.Find();
            foreach (LCFriendshipRequest req in reqs2) {
                await annoymous2.DeclineFriendshipRequest(req);
            }

            await tcs.Task;

            // annoymous3
            await TDSUser.BecomeWithSessionToken(annoymous3.SessionToken);
            LCQuery<LCObject> query = annoymous3.GetFirendshipQuery();
            Assert.Greater((await query.Find()).Count, 0);
        }

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

        private static Dictionary<string, object> GenerateRandomWeixinAuthData() {
            return new Dictionary<string, object> {
                { "expires_in", 7200 },
                { "openid", Guid.NewGuid() },
                { "access_token", Guid.NewGuid() }
            };
        }
    }
}
