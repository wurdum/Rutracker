using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Rutracker.Scraper.Tests
{
    [TestFixture]
    public class ForumPageParserTests
    {
        [Test, TestCaseSource("ParserUnderstandsWhereIsTitlesCases")]
        public void ParserUnderstandsWhereIsTitlesTest(string page, int topicsCount) {
            var parser = new ForumPageParser(page);

            var topics = parser.GetTitles();

            Assert.AreEqual(topicsCount, topics.Count());
        }

        public static IEnumerable<TestCaseData> ParserUnderstandsWhereIsTitlesCases {
            get {
                yield return new TestCaseData(Resources.GetResponseText("Page1.html"), 45);
                yield return new TestCaseData(Resources.GetResponseText("Page2.html"), 50);
            }
        }
    }
}