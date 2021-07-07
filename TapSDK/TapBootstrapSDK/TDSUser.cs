using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LeanCloud.Storage;
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

        public static new async Task<TDSUser> GetCurrent()
        {
            LCUser user = await LCUser.GetCurrent();
            return user as TDSUser;
        }

        public static new async Task<TDSUser> BecomeWithSessionToken(string sessionToken) {
            return (await LCUser.BecomeWithSessionToken(sessionToken)) as TDSUser;
        }

        public static new async Task<TDSUser> LoginWithAuthData(Dictionary<string, object> authData, string platform,
            LCUserAuthDataLoginOption option = null)
        {
            return (await LCUser.LoginWithAuthData(authData, platform, option)) as TDSUser;
        }

        public static new async Task<LCUser> LoginWithAuthDataAndUnionId(Dictionary<string, object> authData,
            string platform, string unionId,
            LCUserAuthDataLoginOption option = null)
        {
            return (await LCUser.LoginWithAuthDataAndUnionId(authData, platform, unionId, option)) as TDSUser;
        }

        public static new async Task<LCUser> LoginAnonymously()
        {
            return (await LCUser.LoginAnonymously()) as TDSUser;
        }

        public static async Task<TDSUser> LoginWithTapTap()
        {
            Dictionary<string, object> authData = await LoginTapTap();
            LCUser user = await LoginWithAuthData(authData, "taptap");
            return user as TDSUser;
        }

        public static new LCQuery<TDSUser> GetQuery()
        {
            return new LCQuery<TDSUser>(CLASS_NAME);
        }

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
    }
}