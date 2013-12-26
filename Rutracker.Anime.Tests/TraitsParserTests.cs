using System.Collections.Generic;
using NUnit.Framework;
using Rutracker.Anime.Models;
using Rutracker.Anime.Parser.Parts;

namespace Rutracker.Anime.Tests
{
    [TestFixture]
    public class TraitsParserTests
    {
        private readonly TraitsParser _traitsParser = new TraitsParser();

        [Test, TestCaseSource("MainTestCases")]
        public void MainTest(string part, Traits expected) {
            var actual = _traitsParser.Parse(part);

            Assert.AreEqual(expected, actual);
        }

        private static IEnumerable<TestCaseData> MainTestCases {
            get {
                yield return new TestCaseData("2011 г., Комедия, Повседневность, Школа, BDRip", new Traits(2011, new[] { "Комедия", "Повседневность", "Школа" }, "BDRip"));
                yield return new TestCaseData("2009 г., вампиры, ужасы, триллер, BDRemux", new Traits(2009, new[] { "вампиры", "ужасы", "триллер" }, "BDRemux"));
                yield return new TestCaseData("2009 г., самурайский боевик, BDRip", new Traits(2009, new[] { "самурайский боевик" }, "BDRip"));
                yield return new TestCaseData("1992-1998 гг., приключения, фантастика, меха, BDRip", new Traits(1992, new[] { "приключения", "фантастика", "меха" }, "BDRip"));
                yield return new TestCaseData("2009 г, комедия, HDTVRip", new Traits(2009, new[] { "комедия" }, "HDTVRip"));
                yield return new TestCaseData("2009, вампиры, ужасы, триллер, BDRemux", new Traits(2009, new[] { "вампиры", "ужасы", "триллер" }, "BDRemux"));
                yield return new TestCaseData("2009 г, комедия", null);
            }
        }
    }
}