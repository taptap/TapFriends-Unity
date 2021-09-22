using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using TapTap.Support;

namespace TapDeskTest
{
    public class TapDeskTest
    {
        private string uuid = "364e7e1e-323e-402e-9892-720ee9813f89";

        [SetUp]
        public async Task Setup()
        {
            TapSupport.Init("https://ticket.sdjdd.com", null, new TapSupportCallback
            {
                UnReadStatusChanged = (b, exception) => { Console.WriteLine($"hasRead:{b} exception:{exception}"); }
            });

            TapSupport.AnonymousLogin(uuid);

            TapSupport.SetDefaultFieldsData(GETDefaultMetaMap());
            TapSupport.SetDefaultFieldsData(GETDefaultFiledsMap());
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void TestTimer()
        {
            var webUrl = TapSupport.GetSupportWebUrl(TapSupportConstants.PathHome);
            Console.WriteLine($"webUrl:{webUrl}");
            Assert.IsNotNull(webUrl);


            var pathCateGory = TapSupport.GetSupportWebUrl(TapSupportConstants.PathCategory + "6108e403928aa97734554ef0",
                GETMetaData(), GETFieldsData());

            Console.WriteLine($"pathCateGory:{pathCateGory}");
        }

        private Dictionary<string, object> GETDefaultMetaMap()
        {
            Dictionary<string, object> testData = new Dictionary<string, object>();
            testData.Add("default_aaa", "default_a");
            testData.Add("default_bbb", 1111);
            testData.Add("default_ccc", true);
            return testData;
        }

        public Dictionary<string, object> GETDefaultFiledsMap()
        {
            Dictionary<string, object> testData = new Dictionary<string, object>();
            testData.Add("612c868565a05a00f081b11c", "default_a");
            testData.Add("6129df889b34d92ea85c59fa", 1111);
            testData.Add("6108ed29928aa97734557912", "服务器1");
            return testData;
        }


        public static Dictionary<string, object> GETMetaData()
        {
            Dictionary<string, object> testData = new Dictionary<string, object>();
            testData.Add("Meta_OS", "iOS 15.1");
            testData.Add("meta_test a", true);
            testData.Add("meta_test b", 1111111);
            testData.Add("ccccc", "abcd");
            return testData;
        }

        public static Dictionary<string, object> GETFieldsData()
        {
            Dictionary<string, object> testData = new Dictionary<string, object>();
            testData.Add("612c868565a05a00f081b11c", "xxxxx");
            testData.Add("6129df889b34d92ea85c59fa", 222);
            testData.Add("6108ed29928aa97734557912", "服务器1");
            return testData;
        }
    }
}