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
            new AudioAndSubsTokenizer(),
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
                        Video = new[] {new VideoContent {
                            Series = new Series(1, 9, 12),
                            AudioAndSubs = new[] { "RUS(int)", "JAP+SUB" }
                        }},
                        Types = new[] { Models.Anime.Type.TV },
                        Traits = new Traits(2013, new[] { "спорт" }, "HDTVRip"),
                        OtherInfo = new[] { "Набэсима Осаму", "Без хардсаба", "720p" }
                    });
                yield return new TestCaseData(
                    "Прекрасные деньки / Деревенская глубинка / Глухомань / Non Non Biyori (Кавамо Синъя) [TV] [01-10 из 12] [Без хардсаба] [RUS(int), JAP+SUB] [2013 г., комедия, повседневность, HDTVRip] [720p]",
                    new Models.Anime {
                        Names = new[] { "Прекрасные деньки", "Деревенская глубинка", "Глухомань", "Non Non Biyori" },
                        Types = new[] { Models.Anime.Type.TV },
                        Video = new[] {new VideoContent {
                            Series = new Series(1, 10, 12),
                            AudioAndSubs = new[] { "RUS(int)", "JAP+SUB" }
                        }},
                        Traits = new Traits(2013, new[] { "комедия", "повседневность" }, "HDTVRip"),
                        OtherInfo = new[] { "Кавамо Синъя", "Без хардсаба", "720p" }
                    });
                yield return new TestCaseData(
                    "Кейон!! [ТВ-2] / Кей-Он / K-On!! (Ямада Наоко) [TV+Special] [24+9 из 24+9] [без хардсабa] [RUS(ext), JAP+SUB] [2010 г., комедия, музыкальный, школа, BDRip] [1080p]",
                    new Models.Anime {
                        Names = new[] { "Кейон!!", "Кей-Он", "K-On!!" },
                        Types = new[] { Models.Anime.Type.TV, Models.Anime.Type.Special },
                        Video = new[] { new VideoContent {
                            Series = new Series(24, 24),
                            AudioAndSubs = new[] { "RUS(ext)", "JAP+SUB" }
                        }},
                        Traits = new Traits(2010, new[] { "комедия", "музыкальный", "школа" }, "BDRip"),
                        OtherInfo = new[] { "Ямада Наоко", "без хардсабa", "1080p" }
                    });
                yield return new TestCaseData(
                    "Сопротивление крови / Strike the Blood [TV] [без хардсаба] [11 из 24] [JAP+SUB] & [9 из 24] [RUS(ext)] [2013 г., приключения, HDTVRip] [720p]",
                    new Models.Anime {
                        Names = new[] { "Сопротивление крови", "Strike the Blood" },
                        Types = new[] { Models.Anime.Type.TV },
                        Video = new[] { new VideoContent {
                            Series = new Series(1, 11, 24),
                            AudioAndSubs = new[] { "JAP+SUB" }
                        }, new VideoContent {
                            Series = new Series(1, 9, 24),
                            AudioAndSubs = new[] { "RUS(ext)" }
                        }},
                        Traits = new Traits(2013, new[] {"приключения"}, "HDTVRip"),
                        OtherInfo = new[] { "без хардсаба", "&", "720p" }
                    });
                yield return new TestCaseData(
                    "Кинжал Камуи / The Dagger of Kamui / Kamui no Ken (Ринтаро)(Без хардсаба)[Movie][RUS(int),JAP+SUB] [1985 г., приключения, драма, история, сёнэн, DVDRip ]",
                    new Models.Anime {
                        Names = new[] { "Кинжал Камуи", "The Dagger of Kamui", "Kamui no Ken" },
                        Types = new[] {Models.Anime.Type.Movie },
                        Video = new[] {
                            new VideoContent {
                                Series = null,
                                AudioAndSubs = new[] {"RUS(int)", "JAP+SUB"}
                            }
                        },
                        Traits = new Traits(1985, new[] { "приключения", "драма", "история", "сёнэн" }, "DVDRip"),
                        OtherInfo = new[] { "Ринтаро", "Без хардсаба" }
                    });
            }
        }
    }
}