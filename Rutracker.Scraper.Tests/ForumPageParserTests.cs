﻿using System.Collections.Generic;
using NUnit.Framework;
using Newtonsoft.Json;

namespace Rutracker.Scraper.Tests
{
    [TestFixture]
    public class ForumPageParserTests
    {
        [Test, TestCaseSource("GetTitlesCases")]
        public void GetTitlesTest(string page, string json) {
            var parser = new ForumPageParser(page);

            var actual = parser.GetTitles();
            var expected = JsonConvert.DeserializeObject<IEnumerable<TopicTitle>>(json);

            CollectionAssert.AreEquivalent(expected, actual);
        }

        public static IEnumerable<TestCaseData> GetTitlesCases {
            get {
                yield return new TestCaseData(Resources.GetResponseText("Page1.html"), Resources.GetJsonText("Page1.html.json"));
                yield return new TestCaseData(Resources.GetResponseText("Page2.html"), Resources.GetJsonText("Page2.html.json"));
                yield return new TestCaseData(Resources.GetResponseText("Page3.html"), Resources.GetJsonText("Page3.html.json"));
            }
        }
    }
}