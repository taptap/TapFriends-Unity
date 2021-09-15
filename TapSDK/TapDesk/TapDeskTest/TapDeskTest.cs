using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using TapTap.Desk;

namespace TapDeskTest
{
    public class TapDeskTest
    {
        private string uuid = "364e7e1e-323e-402e-9892-720ee9813f89";

        [SetUp]
        public async Task Setup()
        {
            TapDesk.Init("https://ticket.sdjdd.com", null, new TapDeskCallback
            {
                UnReadStatusChanged = (b, exception) => { Console.WriteLine($"hasRead:{b} exception:{exception}"); }
            });

            TapDesk.AnonymousLogin(uuid);
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void TestTimer()
        {
            TapDesk.Resume();

            Task.Delay(20000);

            TapDesk.Pause();
        }

        [Test]
        public void GetWebUrl()
        {
            var url = TapDesk.GetDeskWebUrl();
            Console.WriteLine(url);
            Assert.NotNull(url);
        }
    }
}