using NUnit.Framework;

namespace Rutracker.Scraper.Tests
{
    [TestFixture]
    public class HttpProviderTests
    {
        private const string Login = "###";
        private const string Pass = "###";
        private const string LoginUrl = "http://login.rutracker.org/forum/login.php";

        //[Test]
        public void AuthorizationTest() {
            var httpProvider = new HttpProvider();

            var provider = httpProvider.Authorize(LoginUrl, Login, Pass).Result;

            Assert.True(provider.IsAuthorized);
        }
    }
}