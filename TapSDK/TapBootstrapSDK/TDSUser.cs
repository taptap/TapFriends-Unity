using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using LeanCloud;
using LeanCloud.Storage;

namespace TapTap.Bootstrap {
    public class TDSUser : LCUser {
        public new string Username {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }

        public new string Password {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }

        public new string Email {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }

        public new string Mobile {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }

        public new bool EmailVerified {
            get {
                throw new NotImplementedException();
            }
        }

        public new bool MobileVerified {
            get {
                throw new NotImplementedException();
            }
        }

        public new Task<TDSUser> SignUp() {
            throw new NotImplementedException();
        }

        public static new Task RequestLoginSMSCode(string mobile) {
            throw new NotImplementedException();
        }

        public static new Task<TDSUser> SignUpOrLoginByMobilePhone(string mobile, string code) {
            throw new NotImplementedException();
        }

        public static new Task<TDSUser> Login(string username, string password) {
            throw new NotImplementedException();
        }

        public static new Task<TDSUser> LoginByEmail(string email, string password) {
            throw new NotImplementedException();
        }

        public static new Task<TDSUser> LoginByMobilePhoneNumber(string mobile, string password) {
            throw new NotImplementedException();
        }

        public static new Task<TDSUser> LoginBySMSCode(string mobile, string code) {
            throw new NotImplementedException();
        }

        public static new Task RequestEmailVerify(string email) {
            throw new NotImplementedException();
        }

        public static new Task RequestMobilePhoneVerify(string mobile) {
            throw new NotImplementedException();
        }

        public static new Task VerifyMobilePhone(string mobile, string code) {
            throw new NotImplementedException();
        }

        public static new Task<LCUser> BecomeWithSessionToken(string sessionToken) {
            throw new NotImplementedException();
        }

        public static new Task RequestPasswordReset(string email) {
            throw new NotImplementedException();
        }

        public static new Task RequestPasswordResetBySmsCode(string mobile) {
            throw new NotImplementedException();
        }

        public static new Task ResetPasswordBySmsCode(string mobile, string code, string newPassword) {
            throw new NotImplementedException();
        }

        public new Task UpdatePassword(string oldPassword, string newPassword) {
            throw new NotImplementedException();
        }

        public static new Task RequestSMSCodeForUpdatingPhoneNumber(string mobile, int ttl = 360, string captchaToken = null) {
            throw new NotImplementedException();
        }

        public static new Task VerifyCodeForUpdatingPhoneNumber(string mobile, string code) {
            throw new NotImplementedException();
        }

        #region API

        public static new async Task<TDSUser> GetCurrent() {
            LCUser user = await LCUser.GetCurrent();
            return user as TDSUser;
        }

        public static new async Task<TDSUser> LoginWithAuthData(Dictionary<string, object> authData, string platform,
            LCUserAuthDataLoginOption option = null) {
            return (await LCUser.LoginWithAuthData(authData, platform, option)) as TDSUser;
        }

        public static new async Task<LCUser> LoginWithAuthDataAndUnionId(Dictionary<string, object> authData, string platform, string unionId,
            LCUserAuthDataLoginOption option = null) {
            return (await LCUser.LoginWithAuthDataAndUnionId(authData, platform, unionId, option)) as TDSUser;
        }

        public static new async Task<LCUser> LoginAnonymously() {
            return (await LCUser.LoginAnonymously()) as TDSUser;
        }

        public static async Task<TDSUser> LoginWithTapTap(LoginType loginType, string[] permissions) {
            Dictionary<string, object> authData = await LoginTapTap(loginType, permissions);
            LCUser user = await LoginWithAuthData(authData, "taptap");
            return user as TDSUser;
        }

        public static new LCQuery<TDSUser> GetQuery() {
            return new LCQuery<TDSUser>(CLASS_NAME);
        }

        public new async Task<TDSUser> Save(bool fetchWhenSave = false, LCQuery<LCObject> query = null) {
            return (await base.Save(fetchWhenSave, query)) as TDSUser;
        }

        #endregion

        private static Task<Dictionary<string, object>> LoginTapTap(LoginType loginType, string[] permissions) {
            TaskCompletionSource<Dictionary<string, object>> tcs = new TaskCompletionSource<Dictionary<string, object>>();
            ITapLoginResultListener listener = new TapLoginResultListener(tcs);
            TapBootstrap.RegisterLoginResultListener(listener);
            TapBootstrap.Login(loginType, permissions);
            TapBootstrap.RegisterLoginResultListener(null);
            return tcs.Task;
        }

        private class TapLoginResultListener : ITapLoginResultListener {
            private TaskCompletionSource<Dictionary<string, object>> tcs;

            public TapLoginResultListener(TaskCompletionSource<Dictionary<string, object>> tcs) {
                this.tcs = tcs;
            }

            public void OnLoginCancel() {
                tcs.TrySetCanceled();
            }

            public void OnLoginError(TapError error) {
                tcs.TrySetException(new LCException(error.code, error.errorDescription));
            }

            public void OnLoginSuccess(AccessToken token) {
                // AccessToken to Dictionary
                Dictionary<string, object> result = new Dictionary<string, object> {
                    { "kid", token.kid },
                    { "accessToken", token.accessToken },
                    { "macAlgorithm", token.macAlgorithm },
                    { "tokenType", token.tokenType },
                    { "macKey", token.macKey },
                    { "expireIn", token.expireIn }
                };
                tcs.TrySetResult(result);
            }
        }
    }
}
