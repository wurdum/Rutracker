using NUnit.Framework;

namespace Rutracker.Scraper.Tests
{
    [TestFixture(Category = "slow")]
    public class HttpProviderTests
    {
        private const string Login = "###";
        private const string Pass = "###";
        private const string LoginUrl = "http://login.rutracker.org/forum/login.php";

        //[Test]
        public async void AuthorizationTest() {
            var httpProvider = new HttpProvider();

            var provider = await httpProvider.AuthorizeAsync(LoginUrl, Login, Pass);

            Assert.True(provider.IsAuthorized);
        }
    }
}