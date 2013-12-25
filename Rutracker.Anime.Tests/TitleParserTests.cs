using System.Collections.Generic;
using NUnit.Framework;
using Rutracker.Anime.Parser;

namespace Rutracker.Anime.Tests
{
    [TestFixture]
    public class TitleParserTests
    {
        [Test, TestCaseSource("ParseNamesCases")]
        public void ParseNamesTest(string title, IEnumerable<string> names) {
            var titleParser = new TitleParser(PartTypeResolver.Default);

            var animeTitle = titleParser.Parse(title);

            CollectionAssert.AreEquivalent(names, animeTitle.Names);
        }

        public static IEnumerable<TestCaseData> ParseNamesCases {
            get {
                yield return new TestCaseData("Трусливый Велосипедист / Yowamushi Pedal (Набэсима Осаму) [TV] [01-09 из 12] [Без хардсаба] [RUS(int), JAP+SUB] [2013 г., спорт, HDTVRip] [720p]",
                    new[] { "Трусливый Велосипедист", "Yowamushi Pedal" });
                yield return new TestCaseData("Клинок Маню: Тайна сисечного свитка / Manyuu Hiken-chou (Канэко Хираку) [TV] [12 из 12] [Specials] [8 из 8] [Без хардсаба] [RUS(int)] [2011 г., приключения, этти, BDRip] [720p]",
                    new[] { "Клинок Маню: Тайна сисечного свитка", "Manyuu Hiken-chou" });
                yield return new TestCaseData("Баскетбол Куроко (ТВ-2) / Kuroko's Basketball / Kuroko no Basuke [TV] [1-9 из 25] [Без хардсаба] [RUS(int), JAP, SUB] [2013 г., баскетбол, спорт, сёнен, HDTVRip] [720p]",
                    new[] { "Баскетбол Куроко", "Kuroko's Basketball", "Kuroko no Basuke" });
            }
        }
    }
}