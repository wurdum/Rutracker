using NUnit.Framework;

namespace Rutracker.Scraper.Tests
{
    [TestFixture]
    public class UrlBuilderTests
    {
        [Test]
        public void GetLoginPageUrlTest() {
            Assert.AreEqual("http://login.rutracker.org/forum/login.php",
                new UrlBuilder().GetLoginPageUrl());
        }

        [Test]
        [TestCase(1, "http://rutracker.org/forum/viewforum.php?f=1")]
        [TestCase(11111, "http://rutracker.org/forum/viewforum.php?f=11111")]
        public void GetForumPageUrlTest(int id, string expected) {
            Assert.AreEqual(expected, new UrlBuilder()
                .GetForumPageUrl(id));
        }

        [Test]
        [TestCase(2, 0, "http://rutracker.org/forum/viewforum.php?f=2")]
        [TestCase(11111, 100, "http://rutracker.org/forum/viewforum.php?f=11111&start=100")]
        public void GetForumPageStartingAt(int id, int start, string expected) {
            Assert.AreEqual(expected, new UrlBuilder()
                .GetForumPageUrl(id, start));
        }

        [Test]
        [TestCase(1, "http://rutracker.org/forum/viewtopic.php?t=1")]
        [TestCase(11111, "http://rutracker.org/forum/viewtopic.php?t=11111")]
        public void GetTopicPageUrlTest(int id, string expected) {
            Assert.AreEqual(expected, new UrlBuilder()
                .GetTopicPageUrl(id));
        }
    }
}