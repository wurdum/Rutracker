using System.Collections.Generic;
using NUnit.Framework;
using Rutracker.Anime.Models;
using Rutracker.Anime.Parser.Parts;

namespace Rutracker.Anime.Tests
{
    [TestFixture]
    public class SeriesParserTests
    {
        private readonly SeriesParser _seriesParser = new SeriesParser();

        [Test, TestCaseSource("MainTestCases")]
        public void MainTest(string part, Series expected) {
            var actual = _seriesParser.Parse(part);

            Assert.AreEqual(expected, actual);
        }

        private static IEnumerable<TestCaseData> MainTestCases {
            get {
                yield return new TestCaseData("12+6 из 12+6", new Series(12, 12));
                yield return new TestCaseData("51 из 51", new Series(51, 51));
                yield return new TestCaseData("08 из 24", new Series(8, 24));
                yield return new TestCaseData("01-12 из 12", new Series(1, 12, 12));
                yield return new TestCaseData("10-12 из 12", new Series(10, 12, 12));
                yield return new TestCaseData("13+3 из 13+3", new Series(1, 13, 13));
                yield return new TestCaseData("1 из >1", new Series(1, 1, null));
                yield return new TestCaseData("6 из >12", new Series(1, 6, null));
            }
        }
    }
}