using System;
using System.Collections.Generic;

namespace TapTap.License
{
    public interface ITapLicense
    {
        void SetLicencesCallback(ITapLicenseCallback callback);
    
        void SetDLCCallback(ITapDlcCallback callback);
    
        void SetDLCCallback(ITapDlcCallback callback, bool checkOnce, string publicKey);
    
        void Check();
    
        void QueryDLC(string[] skus);
    
        void PurchaseDLC(string sku);
    
    }
    public interface ITapLicenseCallback
    {
        void OnLicenseSuccess();
    }

    public interface ITapDlcCallback
    {
        void OnQueryCallBack(TapLicenseQueryCode code, Dictionary<string, object> queryList);
    
        void OnOrderCallBack(string sku, TapLicensePurchasedCode status);
    }
    
    
    public class TapLicenseConstants
    {
        public static string TAP_LICENSE_SERVICE = "TapLicenseService";
    
        public static string TDS_LICENSE_SERVICE_CLZ = "com.taptap.pay.sdk.library.wrapper.TapLicenseService";
    
        public static string TDS_LICENSE_SERVICE_IMPL = "com.taptap.pay.sdk.library.wrapper.TapLicenseServiceImpl";
    }
    

}