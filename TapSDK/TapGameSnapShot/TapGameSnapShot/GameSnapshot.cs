using System;
using System.Threading.Tasks;
using LeanCloud.Storage;

namespace TapTap.GameSnapshot
{
    public class GameSnapshot : LCObject
    {
        public const string CLASS_NAME = "GameSnapshot";

        public string Name
        {
            get => this["name"] as string;
            set => this["name"] = value;
        }

        public string Description
        {
            get => this["description"] as string;
            set => this["description"] = value;
        }

        public string ModifiedAt
        {
            get => this["modifiedAt"] as string;
            set => this["modifiedAt"] = value;
        }

        public long PlayedTime
        {
            get => this["playedTime"] is long ? (long) this["playedTime"] : -1;
            set => this["playedTime"] = value;
        }

        public int ProgressValue
        {
            get => this["progressValue"] is int ? (int) this["progressValue"] : -1;
            set => this["progressValue"] = value;
        }

        public LCFile Cover
        {
            get => this["cover"] as LCFile;
            set => this["cover"] = value;
        }

        public LCFile GameFile
        {
            get => this["gameFile"] as LCFile;
            set => this["gameFile"] = value;
        }

        public LCUser User
        {
            set => this["user"] = value;
        }

        public string CoverFilePath
        {
            get => CoverFilePath;
            set => this["cover"] = new LCFile("_cover", value);
        }

        public string GameFilePath
        {
            get => GameFilePath;
            set => this["gameFile"] = new LCFile("_gameFile", value);
        }

        public GameSnapshot()
            : base(CLASS_NAME)
        {
        }

        public async Task<GameSnapshot> Save()
        {
            var currentUser = await LCUser.GetCurrent();
            if (currentUser == null)
            {
                throw new UnauthorizedAccessException("Not Login");
            }

            CheckArguments();
            var acl = new LCACL();
            acl.SetUserWriteAccess(currentUser, true);
            acl.SetUserReadAccess(currentUser, true);
            ACL = acl;
            User = currentUser;

            GameFile.ACL = acl;
            GameFile = await GameFile.Save();

            if (Cover == null) return await base.Save() as GameSnapshot;

            Cover.ACL = acl;
            Cover = await Cover.Save();
            return await base.Save() as GameSnapshot;
        }

        public static LCQuery<GameSnapshot> GetQuery(LCUser user)
        {
            var query = GetQuery();
            query.Include("cover");
            query.Include("gameFile");
            query.WhereEqualTo("user", user);
            return query;
        }

        public static LCQuery<GameSnapshot> GetQuery() => new LCQuery<GameSnapshot>(CLASS_NAME);

        private void CheckArguments()
        {
            if (string.IsNullOrEmpty(Name)) throw new ArgumentNullException(nameof(Name));
            if (string.IsNullOrEmpty(Description)) throw new ArgumentNullException(nameof(Description));
            if (string.IsNullOrEmpty(ModifiedAt)) throw new ArgumentNullException(nameof(ModifiedAt));
            if (GameFile == null) throw new ArgumentNullException(nameof(GameFile));
        }
    }
}