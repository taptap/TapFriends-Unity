using System;
using System.Threading.Tasks;
using LeanCloud;
using LeanCloud.Storage;
using TapTap.Bootstrap;
using TapTap.Friends;

namespace FriendOnlineSample {
    class Program {
        static async Task Main(string[] args) {
            Console.WriteLine("Hello World!");

            LCApplication.Initialize("0RiAlMny7jiz086FaU",
                "8V8wemqkpkxmAN7qKhvlh6v0pXc8JJzEZe3JFUnU",
                "https://0rialmny.cloud.tds1.tapapis.cn");
            LCObject.RegisterSubclass("_User", () => new TDSUser());
            LCLogger.LogDelegate = Log;

            TDSUser current = await TDSUser.LoginAnonymously();
            Console.WriteLine($"=================== {current.ObjectId} logged in.");

            TDSFriends.FriendStatusChangedDelegate = new TDSFriendStatusChangedDelegate {
                OnNewRequestComing = async req => {
                    await TDSFriends.AcceptFriendshipRequest(req);
                },
                OnFriendOnline = userId => {
                    Console.WriteLine($"{userId} online.");
                },
                OnFriendOffline = userId => {
                    Console.WriteLine($"{userId} offline.");
                }
            };
            await TDSFriends.Online();

            Console.ReadKey(true);
        }

        private static void Log(LCLogLevel level, string info) {
            switch (level) {
                case LCLogLevel.Debug:
                    Console.WriteLine($"[DEBUG] {info}\n");
                    break;
                case LCLogLevel.Warn:
                    Console.WriteLine($"[WARNING] {info}\n");
                    break;
                case LCLogLevel.Error:
                    Console.WriteLine($"[ERROR] {info}\n");
                    break;
                default:
                    Console.WriteLine(info);
                    break;
            }
        }
    }
}
