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
            httpProvider.Setup(h => h.GetPage(Url)).Returns(Page);
            urlBuilder.Setup(u => u.GetForumPageUrl(Id)).Returns(Url);

            var actual = navigator.GetForumPage(Id);

            httpProvider.Verify(h => h.GetPage(Url), Times.Once());
            urlBuilder.Verify(u => u.GetForumPageUrl(Id), Times.Once());
            Assert.AreEqual(Page, actual);
        }

        [Test]
        public void GetTopicPageTest() {
            var httpProvider = new Mock<HttpProvider>();
            var urlBuilder = new Mock<UrlBuilder>();
            var navigator = new Navigator(httpProvider.Object, urlBuilder.Object);
            httpProvider.Setup(h => h.GetPage(Url)).Returns(Page);
            urlBuilder.Setup(u => u.GetTopicPageUrl(Id)).Returns(Url);

            var actual = navigator.GetTopicPage(Id);

            httpProvider.Verify(h => h.GetPage(Url), Times.Once());
            urlBuilder.Verify(u => u.GetTopicPageUrl(Id), Times.Once());
            Assert.AreEqual(Page, actual);
        }
    }
}