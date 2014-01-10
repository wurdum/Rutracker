using System.Collections.Generic;
using NUnit.Framework;
using Rutracker.Anime.Models;
using Rutracker.Anime.Parser;
using Rutracker.Anime.Parser.Tokenizers;

namespace Rutracker.Anime.Tests
{
    [TestFixture(Category = "slow", Ignore = false)]
    public class LexerTests
    {
        private static readonly IEnumerable<TokenizerBase> Tokenizers = new List<TokenizerBase> {
            new NamesTokenizer(), 
            new SeriesTokenizer(),
            new TracksTokenizer(),
            new TraitsTokenizer(), 
            new TypesTokenizer(), 
            new InfoTokenizer() 
        };

        private readonly Lexer _lexer = new Lexer(new Scanner(Tokenizers), Tokenizers);

        [Test, TestCaseSource("MainTestCases")]
        public void MainTest(string title, Models.Anime expected) {
            var actual = _lexer.Parse(title);

            Assert.AreEqual(expected, actual);
        }

        private static IEnumerable<TestCaseData> MainTestCases {
            get {
                yield return new TestCaseData(
                    "Трусливый Велосипедист / Yowamushi Pedal (Набэсима Осаму) [TV] [01-09 из 12] [Без хардсаба] [RUS(int), JAP+SUB] [2013 г., спорт, HDTVRip] [720p]",
                    new Models.Anime {
                        Names = new[] { "Трусливый Велосипедист", "Yowamushi Pedal" },
                        Types = new[] { Models.Anime.Type.TV },
                        Series = new Series(1, 9, 12),
                        Tracks = new[] { "RUS(int)", "JAP+SUB" },
                        Traits = new Traits(2013, new[] { "спорт" }, "HDTVRip")
                    });
                yield return new TestCaseData(
                    "Прекрасные деньки / Деревенская глубинка / Глухомань / Non Non Biyori (Кавамо Синъя) [TV] [01-10 из 12] [Без хардсаба] [RUS(int), JAP+SUB] [2013 г., комедия, повседневность, HDTVRip] [720p]",
                    new Models.Anime {
                        Names = new[] { "Прекрасные деньки", "Деревенская глубинка", "Глухомань", "Non Non Biyori" },
                        Types = new[] { Models.Anime.Type.TV },
                        Series = new Series(1, 10, 12),
                        Tracks = new[] { "RUS(int)", "JAP+SUB" },
                        Traits = new Traits(2013, new[] { "комедия", "повседневность" }, "HDTVRip")
                    });
                yield return new TestCaseData(
                    "Кейон!! [ТВ-2] / Кей-Он / K-On!! (Ямада Наоко) [TV+Special] [24+9 из 24+9] [без хардсабa] [RUS(ext), JAP+SUB] [2010 г., комедия, музыкальный, школа, BDRip] [1080p]",
                    new Models.Anime {
                        Names = new[] { "Кейон!!", "Кей-Он", "K-On!!" },
                        Types = new[] { Models.Anime.Type.TV, Models.Anime.Type.Special },
                        Series = new Series(24, 24),
                        Tracks = new[] { "RUS(ext)", "JAP+SUB" },
                        Traits = new Traits(2010, new[] { "комедия", "музыкальный", "школа" }, "BDRip")
                    });
            }
        }
    }
}