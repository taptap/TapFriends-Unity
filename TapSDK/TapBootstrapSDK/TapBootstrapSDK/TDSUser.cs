using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LeanCloud.Storage;
using LeanCloud.LiveQuery;
using TapTap.Login;

namespace TapTap.Bootstrap
{
    public class TDSUser : LCUser
    {
        public new string Username
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public new string Password
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public new string Email
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public new string Mobile
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public new bool EmailVerified
        {
            get { throw new NotImplementedException(); }
        }

        public new bool MobileVerified
        {
            get { throw new NotImplementedException(); }
        }

        public new Task<TDSUser> SignUp()
        {
            throw new NotImplementedException();
        }

        public static new Task RequestLoginSMSCode(string mobile)
        {
            throw new NotImplementedException();
        }

        public static new Task<TDSUser> SignUpOrLoginByMobilePhone(string mobile, string code)
        {
            throw new NotImplementedException();
        }

        public static new Task<TDSUser> Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public static new Task<TDSUser> LoginByEmail(string email, string password)
        {
            throw new NotImplementedException();
        }

        public static new Task<TDSUser> LoginByMobilePhoneNumber(string mobile, string password)
        {
            throw new NotImplementedException();
        }

        public static new Task<TDSUser> LoginBySMSCode(string mobile, string code)
        {
            throw new NotImplementedException();
        }

        public static new Task RequestEmailVerify(string email)
        {
            throw new NotImplementedException();
        }

        public static new Task RequestMobilePhoneVerify(string mobile)
        {
            throw new NotImplementedException();
        }

        public static new Task VerifyMobilePhone(string mobile, string code)
        {
            throw new NotImplementedException();
        }

        public static new Task RequestPasswordReset(string email)
        {
            throw new NotImplementedException();
        }

        public static new Task RequestPasswordResetBySmsCode(string mobile)
        {
            throw new NotImplementedException();
        }

        public static new Task ResetPasswordBySmsCode(string mobile, string code, string newPassword)
        {
            throw new NotImplementedException();
        }

        public new Task UpdatePassword(string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public static new Task RequestSMSCodeForUpdatingPhoneNumber(string mobile, int ttl = 360,
            string captchaToken = null)
        {
            throw new NotImplementedException();
        }

        public static new Task VerifyCodeForUpdatingPhoneNumber(string mobile, string code)
        {
            throw new NotImplementedException();
        }

        #region API

        /// <summary>
        /// Gets the currently logged in TDSUser with a valid session, from
        /// memory or disk if necessary.
        /// </summary>
        /// <returns></returns>
        public static new async Task<TDSUser> GetCurrent()
        {
            LCUser user = await LCUser.GetCurrent();
            return user as TDSUser;
        }

        /// <summary>
        /// Signs in a user with a sessionToken.
        /// </summary>
        /// <param name="sessionToken"></param>
        /// <returns></returns>
        public static new async Task<TDSUser> BecomeWithSessionToken(string sessionToken)
        {
            return (await LCUser.BecomeWithSessionToken(sessionToken)) as TDSUser;
        }

        /// <summary>
        /// Signs up or signs in a user with third party authData.
        /// </summary>
        /// <param name="authData"></param>
        /// <param name="platform"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static new async Task<TDSUser> LoginWithAuthData(Dictionary<string, object> authData, string platform,
            LCUserAuthDataLoginOption option = null)
        {
            return (await LCUser.LoginWithAuthData(authData, platform, option)) as TDSUser;
        }

        /// <summary>
        /// Signs up or signs in a user with third party authData and unionId.
        /// </summary>
        /// <param name="authData"></param>
        /// <param name="platform"></param>
        /// <param name="unionId"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static new async Task<TDSUser> LoginWithAuthDataAndUnionId(Dictionary<string, object> authData,
            string platform, string unionId,
            LCUserAuthDataLoginOption option = null)
        {
            return (await LCUser.LoginWithAuthDataAndUnionId(authData, platform, unionId, option)) as TDSUser;
        }

        /// <summary>
        /// Creates an anonymous user.
        /// </summary>
        /// <returns></returns>
        public static new async Task<TDSUser> LoginAnonymously()
        {
            return (await LCUser.LoginAnonymously()) as TDSUser;
        }

        /// <summary>
        /// Signs up or signs in a user with TapTap.
        /// </summary>
        /// <returns></returns>
        public static async Task<TDSUser> LoginWithTapTap()
        {
            Dictionary<string, object> authData = await LoginTapTap();
            LCUser user = await LoginWithAuthData(authData, "taptap");
            return user as TDSUser;
        }

        /// <summary>
        /// Logs out the currently logged in user session.
        /// </summary>
        public static new async Task Logout()
        {
            TapLogin.Logout();
            await LCUser.Logout();
        }

        /// <summary>
        /// Constructs a LCQuery for TDSUser.
        /// </summary>
        /// <returns></returns>
        public static new LCQuery<TDSUser> GetQuery()
        {
            return new LCQuery<TDSUser>(CLASS_NAME);
        }

        /// <summary>
        /// Save this user to the cloud.
        /// </summary>
        /// <param name="fetchWhenSave"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public new async Task<TDSUser> Save(bool fetchWhenSave = false, LCQuery<LCObject> query = null)
        {
            return (await base.Save(fetchWhenSave, query)) as TDSUser;
        }

        #endregion

        private static async Task<Dictionary<string, object>> LoginTapTap()
        {
            var token = await TapLogin.Login();

            var profile = await TapLogin.GetProfile();

            var result = new Dictionary<string, object>
            {
                {"kid", token.kid},
                {"access_token", token.accessToken},
                {"token_type", token.tokenType},
                {"mac_key", token.macKey},
                {"mac_algorithm", token.macAlgorithm},

                {"openid", profile.openid},
                {"name", profile.name},
                {"avatar", profile.avatar},
                {"unionid", profile.unionid}
            };
            return result;
        }

        #region Friendship

        public class FriendshipNotification {
            public Action<LCFriendshipRequest> OnNewRequestComing { get; set; }
            public Action<LCFriendshipRequest> OnRequestAccepted { get; set; }
            public Action<LCFriendshipRequest> OnRequestDeclined { get; set; }
        }

        private LCLiveQuery friendshipLivequery;

        public Task ApplyFriendship(TDSUser user, Dictionary<string, object> attributes = null) {
            if (user == null) {
                throw new ArgumentNullException(nameof(user));
            }
            return LCFriendship.Request(user.ObjectId, attributes);
        }

        public Task AcceptFriendshipRequest(LCFriendshipRequest request, Dictionary<string, object> attributes = null) {
            return LCFriendship.AcceptRequest(request, attributes);
        }

        public Task DeclineFriendshipRequest(LCFriendshipRequest request) {
            return LCFriendship.DeclineRequest(request);
        }

        public Task DeleteFriendshipRequest(LCFriendshipRequest request) {
            if (request == null) {
                throw new ArgumentNullException("request is null.");
            }
            return request.Delete();
        }

        public LCQuery<LCFriendshipRequest> GetFriendshipRequestQuery(int status, bool includeTargetUser, bool reachToCurrentUser) {
            List<string> statusList = new List<string>();
            if ((status & LCFriendshipRequest.STATUS_PENDING) == LCFriendshipRequest.STATUS_PENDING) {
                statusList.Add("pending");
            }
            if ((status & LCFriendshipRequest.STATUS_ACCEPTED) == LCFriendshipRequest.STATUS_ACCEPTED) {
                statusList.Add("accepted");
            }
            if ((status & LCFriendshipRequest.STATUS_DECLINED) == LCFriendshipRequest.STATUS_DECLINED) {
                statusList.Add("declined");
            }
            if (statusList.Count < 1) {
                throw new ArgumentException("status is invalid.");
            }

            LCQuery<LCFriendshipRequest> query = LCFriendshipRequest.GetQuery();
            query.WhereContainedIn("status", statusList);
            if (reachToCurrentUser) {
                query.WhereEqualTo("friend", this);
                if (includeTargetUser) {
                    query.Include("user");
                }
            } else {
                query.WhereEqualTo("user", this);
                if (includeTargetUser) {
                    query.Include("friend");
                }
            }
            query.AddDescendingOrder("updatedAt");
            return query;
        }

        public LCQuery<LCObject> GetFriendshipQuery() {
            if (string.IsNullOrEmpty(ObjectId)) {
                throw new ArgumentNullException("user objectId is null.");
            }
            LCQuery<LCObject> query = new LCQuery<LCObject>("_Followee")
                .WhereEqualTo("user", this)
                .WhereEqualTo("friendStatus", true)
                .Include("followee");
            return query;
        }


        public async Task RegisterFriendshipNotification(FriendshipNotification notification) {
            if (friendshipLivequery != null) {
                // 避免重复注册
                return;
            }

            // 构建 LiveQuery
            LCQuery<LCFriendshipRequest> selfRequestQuery = new LCQuery<LCFriendshipRequest>(LCFriendshipRequest.CLASS_NAME)
                .WhereEqualTo("user", this);
            LCQuery<LCFriendshipRequest> otherRequestQuery = new LCQuery<LCFriendshipRequest>(LCFriendshipRequest.CLASS_NAME)
                .WhereEqualTo("friend", this);
            LCQuery<LCFriendshipRequest> allQuery = LCQuery<LCFriendshipRequest>.Or(new LCQuery<LCFriendshipRequest>[] {
                selfRequestQuery, otherRequestQuery
            });

            friendshipLivequery = await allQuery.Subscribe();
            friendshipLivequery.OnCreate = obj => {
                if (!(obj is LCFriendshipRequest req)) {
                    return;
                }

                LCUser friend = req.Friend;
                if (friend == null || friend.ObjectId != ObjectId) {
                    return;
                }

                notification.OnNewRequestComing(req);
            };
            friendshipLivequery.OnUpdate = (obj, keys) => {
                if (!(obj is LCFriendshipRequest req)) {
                    return;
                }
                if (keys == null || !keys.Contains("status")) {
                    return;
                }

                LCUser user = req.User;
                if (user == null || user.ObjectId != ObjectId) {
                    return;
                }

                string status = req.Status;
                if (status == "accepted") {
                    notification.OnRequestAccepted(req);
                } else if (status == "declined") {
                    notification.OnRequestDeclined(req);
                }
            };
        }

        public async Task UnregisterFriendshipNotification() {
            if (friendshipLivequery == null) {
                return;
            }

            await friendshipLivequery.Unsubscribe();
            friendshipLivequery = null;
        }

        #endregion
    }
}