using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using LeanCloud.Storage;

namespace TapTap.Bootstrap
{
    public class TapGameSave : LCObject
    {
        public const string CLASS_NAME = "GameSave";

        public string Name
        {
            get => this["name"] as string;
            set => this["name"] = value;
        }

        public string Summary
        {
            get => this["summary"] as string;
            set => this["summary"] = value;
        }

        public DateTime ModifiedAt
        {
            get => this["modifiedAt"] is DateTime ? (DateTime) this["modifiedAt"] : default;
            set => this["modifiedAt"] = value;
        }

        public double PlayedTime
        {
            get => this["playedTime"] is double ? (double) this["playedTime"] : -1d;
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
            set => this["cover"] = new LCFile(Path.GetFileName(value), value);
        }

        public string GameFilePath
        {
            set => this["gameFile"] = new LCFile(Path.GetFileName(value), value);
        }

        public TapGameSave()
            : base(CLASS_NAME)
        {
        }

        public async Task<TapGameSave> Save()
        {
            var currentUser = await LCUser.GetCurrent();
            if (currentUser == null) throw new UnauthorizedAccessException("Not Login");
            CheckArguments();
            var acl = new LCACL();
            acl.SetUserWriteAccess(currentUser, true);
            acl.SetUserReadAccess(currentUser, true);
            ACL = acl;
            User = currentUser;
            if (Cover != null)
            {
                Cover.ACL = acl;
            }

            GameFile.ACL = acl;
            return await base.Save() as TapGameSave;
        }

        public static async Task<ReadOnlyCollection<TapGameSave>> GetCurrentUserGameSaves()
        {
            var user = await LCUser.GetCurrent();
            if (user == null) throw new UnauthorizedAccessException("Not Login");
            return await GetQueryWithUser(user).Find();
        }

        public static LCQuery<TapGameSave> GetQuery() => new LCQuery<TapGameSave>(CLASS_NAME);

        public static LCQuery<TapGameSave> GetQueryWithUser(LCUser user)
        {
            var query = GetQuery();
            query.Include("cover");
            query.Include("gameFile");
            query.WhereEqualTo("user", user);
            return query;
        }

        private void CheckArguments()
        {
            if (string.IsNullOrEmpty(Name)) throw new ArgumentNullException(nameof(Name));
            if (string.IsNullOrEmpty(Summary)) throw new ArgumentNullException(nameof(Summary));
            if (Summary.Length > 1000) throw new ArgumentOutOfRangeException(nameof(Summary));
            if (GameFile == null) throw new ArgumentNullException(nameof(GameFile));
            if (Cover == null) return;
            if (!GameSaveMimeType.SupportImageMimeType.Contains(Cover.MimeType))
                throw new ArgumentException("GameSave Cover File must be png or jpg.");
        }

        private static class GameSaveMimeType
        {
            internal static readonly List<string> SupportImageMimeType = new List<string>
            {
                "image/png", "image/jpg"
            };
        }
    }
}