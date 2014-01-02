using System.Collections.Generic;
using NUnit.Framework;
using Rutracker.Anime.Models;
using Rutracker.Anime.Parser;
using Rutracker.Anime.Parser.Tokenizers;

namespace Rutracker.Anime.Tests
{
    [TestFixture(Category = "slow", Ignore = false)]
    public class MainTests
    {
        private readonly TitleParser _titleParser = new TitleParser(PartTypeResolver.Default, new PartParsers {
            SeriesTokenizer = new SeriesTokenizer(),
            TracksTokenizer = new TracksTokenizer(),
            TraitsTokenizer = new TraitsTokenizer(),
            TypesTokenizer = new TypesTokenizer()
        });

        [Test, TestCaseSource("MainTestCases")]
        public void MainTest(string title, Models.Anime expected) {
            var actual = _titleParser.Parse(title);

            Assert.AreEqual(expected, actual);
        }

        private static IEnumerable<TestCaseData> MainTestCases {
            get {
                yield return new TestCaseData(
                    "Трусливый Велосипедист / Yowamushi Pedal (Набэсима Осаму) [TV] [01-09 из 12] [Без хардсаба] [RUS(int), JAP+SUB] [2013 г., спорт, HDTVRip] [720p]",
                    new Models.Anime {
                        Names = new[] { "Трусливый Велосипедист", "Yowamushi Pedal" },
                        Types = new[] {Models.Anime.Type.TV },
                        Series = new Series(1, 9, 12),
                        Tracks = new[] { "RUS(int)", "JAP+SUB" },
                        Traits = new Traits(2013, new[] { "спорт" }, "HDTVRip")
                    });
            }
        }
    }
}