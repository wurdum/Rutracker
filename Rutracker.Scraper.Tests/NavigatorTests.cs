using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace Rutracker.Scraper.Tests
{
    [TestFixture]
    public class NavigatorTests
    {
        private const int Id = 1;
        private const string Url = "xxx";
        private const string Page = "ppp";

        [Test]
        public void GetForumPageTest() {
            var httpProvider = new Mock<HttpProvider>();
            var urlBuilder = new Mock<UrlBuilder>();
            var navigator = new Navigator(httpProvider.Object, urlBuilder.Object);
            httpProvider.Setup(h => h.GetPageAsync(Url)).Returns(Task.FromResult(Page));
            urlBuilder.Setup(u => u.GetForumPageUrl(Id)).Returns(Url);

            var actual = navigator.GetForumPageAsync(Id).Result;

            httpProvider.Verify(h => h.GetPageAsync(Url), Times.Once());
            urlBuilder.Verify(u => u.GetForumPageUrl(Id), Times.Once());
            Assert.AreEqual(Page, actual);
        }

        [Test]
        public void GetTopicPageTest() {
            var httpProvider = new Mock<HttpProvider>();
            var urlBuilder = new Mock<UrlBuilder>();
            var navigator = new Navigator(httpProvider.Object, urlBuilder.Object);
            httpProvider.Setup(h => h.GetPageAsync(Url)).Returns(Task.FromResult(Page));
            urlBuilder.Setup(u => u.GetTopicPageUrl(Id)).Returns(Url);

            var actual = navigator.GetTopicPageAsync(Id).Result;

            httpProvider.Verify(h => h.GetPageAsync(Url), Times.Once());
            urlBuilder.Verify(u => u.GetTopicPageUrl(Id), Times.Once());
            Assert.AreEqual(Page, actual);
        }
    }
}