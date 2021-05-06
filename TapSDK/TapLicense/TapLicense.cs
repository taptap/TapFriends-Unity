using System;

namespace TapTap.License
{
    public class TapLicense
    {
        public static void SetLicenseCallBack(ITapLicenseCallback callback)
        {
            TapLicenseImpl.GetInstance().SetLicencesCallback(callback);
        }

        public static void SetDlcCallback(ITapDlcCallback callback)
        {
            TapLicenseImpl.GetInstance().SetDlcCallback(callback);
        }

        public static void SetDlcCallback(ITapDlcCallback callback, bool checkOnce, string publicKey)
        {
            TapLicenseImpl.GetInstance().SetDlcCallback(callback, checkOnce, publicKey);
        }

        public static void Check()
        {
            TapLicenseImpl.GetInstance().Check();
        }

        public static void QueryDlc(string[] dlcList)
        {
            TapLicenseImpl.GetInstance().QueryDlc(dlcList);
        }

        public static void PurchaseDlc(string dlc)
        {
            TapLicenseImpl.GetInstance().PurchaseDlc(dlc);
        }
    }
}