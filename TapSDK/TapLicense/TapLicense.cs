using System;

namespace TapTap.License
{
    public class TapLicense
    {
        public static void SetLicenseCallBack(ITapLicenseCallback callback)
        {
            TapLicenseImpl.GetInstance().SetLicencesCallback(callback);
        }

        public static void SetDLCCallback(ITapDlcCallback callback)
        {
            TapLicenseImpl.GetInstance().SetDLCCallback(callback);
        }

        public static void SetDLCCallback(ITapDlcCallback callback, bool checkOnce, string publicKey)
        {
            TapLicenseImpl.GetInstance().SetDLCCallback(callback, checkOnce, publicKey);
        }

        public static void Check()
        {
            TapLicenseImpl.GetInstance().Check();
        }

        public static void QueryDLC(string[] dlcList)
        {
            TapLicenseImpl.GetInstance().QueryDLC(dlcList);
        }

        public static void PurchaseDLC(string dlc)
        {
            TapLicenseImpl.GetInstance().PurchaseDLC(dlc);
        }
    }
}