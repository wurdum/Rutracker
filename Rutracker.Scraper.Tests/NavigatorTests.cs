using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace Rutracker.Scraper.Tests
{
    [TestFixture]
    public class NavigatorTests
    {
        private const int Id = 1;
        private const string Url1 = "xxx1";
        private const string Url2 = "xxx2";
        private const string Page1 = "ppp1";
        private const string Page2 = "ppp2";

        [Test]
        public void GetForumPageTest() {
            var httpProvider = new Mock<HttpProvider>();
            var urlBuilder = new Mock<UrlBuilder>();
            var navigator = new Navigator(httpProvider.Object, urlBuilder.Object);
            httpProvider.Setup(h => h.GetPageAsync(Url1)).Returns(Task.FromResult(Page1));
            urlBuilder.Setup(u => u.GetForumPageUrl(Id)).Returns(Url1);

            var actual = navigator.GetForumPageAsync(Id).Result;

            httpProvider.Verify(h => h.GetPageAsync(Url1), Times.Once());
            urlBuilder.Verify(u => u.GetForumPageUrl(Id), Times.Once());
            Assert.AreEqual(Page1, actual);
        }

        [Test]
        public void GetSeveralForumPagesTest() {
            var httpProvider = new Mock<HttpProvider>();
            var urlBuilder = new Mock<UrlBuilder>();
            var navigator = new Navigator(httpProvider.Object, urlBuilder.Object);
            httpProvider.Setup(h => h.GetPageAsync(Url1)).Returns(Task.FromResult(Page1));
            httpProvider.Setup(h => h.GetPageAsync(Url2)).Returns(Task.FromResult(Page2));
            urlBuilder.Setup(u => u.GetForumPageUrl(Id, 0)).Returns(Url1);
            urlBuilder.Setup(u => u.GetForumPageUrl(Id, Navigator.TopicsPerPage)).Returns(Url2);

            var actual = navigator.GetForumPagesAsync(Id, new[] {0, 1}).Result;

            httpProvider.Verify(h => h.GetPageAsync(Url1), Times.Once());
            httpProvider.Verify(h => h.GetPageAsync(Url2), Times.Once());
            urlBuilder.Verify(u => u.GetForumPageUrl(Id, 0), Times.Once());
            urlBuilder.Verify(u => u.GetForumPageUrl(Id, Navigator.TopicsPerPage), Times.Once());
            CollectionAssert.AreEqual(new List<string> {Page1, Page2}, actual);
        }

        [Test]
        public void GetTopicPageTest() {
            var httpProvider = new Mock<HttpProvider>();
            var urlBuilder = new Mock<UrlBuilder>();
            var navigator = new Navigator(httpProvider.Object, urlBuilder.Object);
            httpProvider.Setup(h => h.GetPageAsync(Url1)).Returns(Task.FromResult(Page1));
            urlBuilder.Setup(u => u.GetTopicPageUrl(Id)).Returns(Url1);

            var actual = navigator.GetTopicPageAsync(Id).Result;

            httpProvider.Verify(h => h.GetPageAsync(Url1), Times.Once());
            urlBuilder.Verify(u => u.GetTopicPageUrl(Id), Times.Once());
            Assert.AreEqual(Page1, actual);
        }

        //[Test]
        public async void CollectPages() {
            var httpProvider = new HttpProvider();
            var urlBuilder = new UrlBuilder();
            var navigator = new Navigator(httpProvider, urlBuilder);

            var pages = await navigator.GetForumPagesAsync(1105, 0, 1, 2);

            var counter = 1;
            foreach (var page in pages) {
                Console.WriteLine(page);
                File.WriteAllText("page" + counter++ + ".html", page, Encoding.UTF8);
            }
        }
    }
}