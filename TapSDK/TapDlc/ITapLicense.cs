using System.Collections.Generic;

namespace TapTap.Dlc
{
    public interface ITapLicense
        {
            void SetLicencesCallback(ITapLicenseCallback callback);
    
            void SetDlcCallback(ITapDlcCallback callback);
    
            void SetDlcCallback(ITapDlcCallback callback, bool checkOnce, string publicKey);
    
            void Check();
    
            void QueryDlc(string[] dlcList);
    
            void PurchaseDlc(string dlc);
    
        }
    
        public interface ITapLicenseCallback
        {
            void OnLicenseSuccess();
        }
    
        public interface ITapDlcCallback
        {
            void OnQueryCallBack(int code, Dictionary<string, object> queryList);
    
            void OnOrderCallBack(string sku, int status);
        }
    
    
        public class TapLicenseConstants
        {
            public static string TAP_LICENSE_SERVICE = "TapLicenseService";
    
            public static string TDS_LICENSE_SERVICE_CLZ = "com.taptap.pay.sdk.library.wrapper.TapLicenseService";
    
            public static string TDS_LICENSE_SERVICE_IMPL = "com.taptap.pay.sdk.library.wrapper.TapLicenseServiceImpl";
        }
    

}