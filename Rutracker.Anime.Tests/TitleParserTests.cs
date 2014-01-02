using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Rutracker.Anime.Models;
using Rutracker.Anime.Parser;
using Rutracker.Anime.Parser.Parts;

namespace Rutracker.Anime.Tests
{
    [TestFixture]
    public class TitleParserTests
    {
        [Test, TestCaseSource("MainTestCases")]
        public void MainTest(string title, IEnumerable<string> names) {
            var titleParser = new TitleParser(PartTypeResolver.Default, new PartParsers {
                SeriesTokenizer = Mock.Of<SeriesTokenizer>(sp => sp.Tokenize(It.IsAny<string>()) == new Series(null, null, null)),
                TracksTokenizer = Mock.Of<TracksTokenizer>(tp => tp.Tokenize(It.IsAny<string>()) == new string[0]),
                TraitsTokenizer = Mock.Of<TraitsTokenizer>(tp => tp.Tokenize(It.IsAny<string>()) == new Traits(null, null, null)),
                TypesParser = Mock.Of<TypesParser>(tp => tp.Parse(It.IsAny<string>()) == new Models.Anime.Type[0])
            });

            var animeTitle = titleParser.Parse(title);

            CollectionAssert.AreEquivalent(names, animeTitle.Names);
        }

        public static IEnumerable<TestCaseData> MainTestCases {
            get {
                yield return new TestCaseData("Трусливый Велосипедист / Yowamushi Pedal (Набэсима Осаму) [TV] [01-09 из 12] [Без хардсаба] [RUS(int), JAP+SUB] [2013 г., спорт, HDTVRip] [720p]",
                    new[] { "Трусливый Велосипедист", "Yowamushi Pedal" });
                yield return new TestCaseData("Клинок Маню: Тайна сисечного свитка / Manyuu Hiken-chou (Канэко Хираку) [TV] [12 из 12] [Specials] [8 из 8] [Без хардсаба] [RUS(int)] [2011 г., приключения, этти, BDRip] [720p]",
                    new[] { "Клинок Маню: Тайна сисечного свитка", "Manyuu Hiken-chou" });
                yield return new TestCaseData("Баскетбол Куроко (ТВ-2) / Kuroko's Basketball / Kuroko no Basuke [TV] [1-9 из 25] [Без хардсаба] [RUS(int), JAP, SUB] [2013 г., баскетбол, спорт, сёнен, HDTVRip] [720p]",
                    new[] { "Баскетбол Куроко", "Kuroko's Basketball", "Kuroko no Basuke" });
                yield return new TestCaseData("Чудесные дни; Удивительные дни; Фантастические дни / Wonderful Days; Sky Blue (Moon-saeng Kim) [Movie][без хардсаба][RUS(ext),KOR(int)+SUB][2003 г., Фантастика, DVDRip] Режиссерская версия / Director's Cut",
                    new[] { "Чудесные дни; Удивительные дни; Фантастические дни", "Wonderful Days; Sky Blue", "Чудесные дни", "Удивительные дни", "Фантастические дни", "Wonderful Days", "Sky Blue" });
            }
        }
    }
}