using System;
using System.Threading.Tasks;
using LeanCloud;
using LeanCloud.Storage;
using NUnit.Framework;
using TapTap.GameSnapshot;

namespace TapGameSnapshot.Test
{
    public class TapGameSnapshotTest
    {
        private readonly string dll = "../../../../../../UnityLibs/UnityEngine.dll";
        private readonly string dllUpdate = "../../../../../../UnityLibs/UnityEditor_update.dll";
        private readonly string pic = "../../../../../../UnityLibs/hello.png";
        private readonly string PicUpdate = "../../../../../../UnityLibs/update.webp";

        [SetUp]
        public async Task Setup()
        {
            LCApplication.Initialize("0RiAlMny7jiz086FaU",
                "8V8wemqkpkxmAN7qKhvlh6v0pXc8JJzEZe3JFUnU",
                "https://0rialmny.cloud.tds1.tapapis.cn");
            LCObject.RegisterSubclass(GameSnapshot.CLASS_NAME, () => new GameSnapshot());
            LCLogger.LogDelegate = Log;

            await Login();
        }

        [TearDown]
        public async Task TearDown()
        {
            LCLogger.LogDelegate = null;
            await LCUser.Logout();
        }

        [Test]
        public async Task SaveSnapshot()
        {
            var snapshot = Constructor();
            await snapshot.Save();
            Assert.NotNull(snapshot.ObjectId);
        }

        [Test]
        public async Task SaveSnapshotWithArguments()
        {
            var snapshot = new GameSnapshot();
            try
            {
                await snapshot.Save();
            }
            catch (ArgumentNullException e)
            {
                Assert.NotNull(e.Message);
            }
        }

        [Test]
        public async Task GetAllSnapshot()
        {
            try
            {
                await Login();
                var collection = await GameSnapshot.GetCurrentUserSnapshot();
                Assert.NotNull(collection);
                foreach (var snapshot in collection)
                {
                    Assert.NotNull(snapshot.ObjectId);
                    Assert.NotNull(snapshot.Name);
                    Assert.NotNull(snapshot.Description);
                    Assert.NotNull(snapshot.GameFile);
                    var file = snapshot["gameFile"] as LCFile;
                    Assert.NotNull(file);
                    Assert.NotNull(file.ObjectId);
                }
            }
            catch (LCException e)
            {
                Log(LCLogLevel.Debug, e.ToString());
            }
        }

        [Test]
        public async Task SaveSnapShotWithLogin()
        {
            var user = await LCUser.GetCurrent();
            if (user != null)
            {
                await LCUser.Logout();
            }

            try
            {
                var snapshot = Constructor();
                await snapshot.Save();
            }
            catch (UnauthorizedAccessException e)
            {
                Log(LCLogLevel.Debug, e.Message);
                Assert.AreEqual("Not Login", e.Message);
            }
        }

        [Test]
        public async Task Delete()
        {
            var user = await LCUser.GetCurrent() ?? await Login();

            var query = await GameSnapshot.GetCurrentUserSnapshot();

            if (query.Count > 0)
            {
                var snapshot = query[0];

                var snapshotObjectId = snapshot.ObjectId;

                await snapshot.Delete();

                var querySnapshot = await new LCQuery<GameSnapshot>(GameSnapshot.CLASS_NAME).Get(snapshotObjectId);

                Assert.Null(querySnapshot.ObjectId);
            }
        }

        [Test]
        public async Task WithOutAclReadSnapshot()
        {
            var user = await LCUser.GetCurrent() ?? await Login();

            var query = await GameSnapshot.GetCurrentUserSnapshot();

            if (query.Count > 0)
            {
                var snapShot = query[0];
                Assert.NotNull(snapShot);
                var snapShotObjectId = snapShot.ObjectId;
                Assert.NotNull(snapShotObjectId);
                try
                {
                    await LCUser.Logout();
                    await OtherUserLogin();
                    var otherUserQuerySnapshot = await GameSnapshot.GetQuery().Get(snapShotObjectId);
                    Assert.Null(otherUserQuerySnapshot);
                }
                catch (LCException e)
                {
                    Assert.AreEqual("Forbidden reading by object's ACL.", e.Message);
                    Log(LCLogLevel.Debug, e.ToString());
                }
            }
        }

        [Test]
        public async Task WithOutAclWriteSnapshot()
        {
            var user = await LCUser.GetCurrent() ?? await Login();

            var query = await GameSnapshot.GetCurrentUserSnapshot();

            if (query.Count > 0)
            {
                var snapShot = query[0];
                Assert.NotNull(snapShot);
                var snapShotObjectId = snapShot.ObjectId;
                Assert.NotNull(snapShotObjectId);
                try
                {
                    await LCUser.Logout();
                    await OtherUserLogin();
                    Log(LCLogLevel.Debug, $"WithOutAclWriteSnapshot:{snapShot}");
                    snapShot.Name = "GameSnapshot_Name_By_Other_user";
                    await snapShot.Save();
                }
                catch (LCException e)
                {
                    Assert.AreEqual("Not Found", e.Message);
                    Log(LCLogLevel.Debug, e.ToString());
                }
            }
        }

        [Test]
        public async Task UpdateGameSnap()
        {
            var user = await LCUser.GetCurrent() ?? await Login();

            await SaveSnapshot();

            var query = await GameSnapshot.GetCurrentUserSnapshot();

            if (query.Count > 0)
            {
                var snapShot = query[0];
                Assert.NotNull(snapShot);
                var snapShotObjectId = snapShot.ObjectId;
                Assert.NotNull(snapShotObjectId);
                try
                {
                    snapShot.Name = "GameSnapshot_Name_Update";
                    snapShot.CoverFilePath = PicUpdate;
                    snapShot.GameFilePath = dllUpdate;
                    await snapShot.Save();

                    var querySnapShot = await GameSnapshot.GetQuery().Get(snapShot.ObjectId);
                    Assert.NotNull(querySnapShot);
                    Assert.AreEqual("GameSnapshot_Name_Update", querySnapShot.Name);
                }
                catch (LCException e)
                {
                    Assert.AreEqual("Not Found", e.Message);
                    Log(LCLogLevel.Debug, e.ToString());
                }
            }
        }

        private static async Task<LCUser> Login()
        {
            return await LCUser.BecomeWithSessionToken("nz30npdqbeovcmvukoxxr2t8d");
        }

        private static async Task<LCUser> OtherUserLogin()
        {
            return await LCUser.LoginAnonymously();
        }
        
        private GameSnapshot Constructor()
        {
            return new GameSnapshot
            {
                Name = "GameSnapshot_Name",
                Description = "GameSnapshot_Description",
                ModifiedAt = DateTime.Now.ToLocalTime(),
                PlayedTime = 1000L,
                ProgressValue = 100,
                CoverFilePath = pic,
                GameFilePath = dll
            };
        }

        private static void Log(LCLogLevel level, string info)
        {
            switch (level)
            {
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