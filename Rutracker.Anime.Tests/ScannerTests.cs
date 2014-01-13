using System.Collections.Generic;
using NUnit.Framework;
using Rutracker.Anime.Models;
using Rutracker.Anime.Parser;
using Rutracker.Anime.Parser.Tokenizers;

namespace Rutracker.Anime.Tests
{
    [TestFixture]
    public class ScannerTests
    {
        private readonly Scanner _scanner = new Scanner(new ITokenEvaluator[] {
            new NamesTokenizer(), 
            new SeriesTokenizer(),
            new AudioAndSubsTokenizer(),
            new TraitsTokenizer(), 
            new TypesTokenizer(), 
            new InfoTokenizer() 
        });

        [Test, TestCaseSource("MainTestCases")]
        public void MainTest(string title, IEnumerable<Lexeme> expected) {
            var actual = _scanner.Scan(title);

            Assert.AreEqual(expected, actual);
        }

        private static IEnumerable<TestCaseData> MainTestCases {
            get {
                yield return new TestCaseData(
                    "Любовные неприятности (OVA) / To Love-Ru: Trouble [OVA] [6 из 6] [Без хардсаба] [JAP, SUB] " +
                    "[2009 г., комедия, романтика, фантастика, этти, BDRip] [1080p]",
                    new[] {
                        new Lexeme(TokenType.Names, "Любовные неприятности (OVA) / To Love-Ru: Trouble"),
                        new Lexeme(TokenType.AnimeType, "[OVA]"),
                        new Lexeme(TokenType.Series, "[6 из 6]"),
                        new Lexeme(TokenType.Info, "[Без хардсаба]"),
                        new Lexeme(TokenType.Tracks, "[JAP, SUB]"),
                        new Lexeme(TokenType.Traits, "[2009 г., комедия, романтика, фантастика, этти, BDRip]"),
                        new Lexeme(TokenType.Info, "[1080p]"),
                    });
                yield return new TestCaseData(
                    "Клинок Королевы (ТВ-3): Восстание / Queen's Blade: Rebellion (Кагэяма Сигэнори) [TV + Special] [12+6 из 12+6] " +
                    "[без хардсаба] [JAP+SUB] [2012 г., боевые искусства, фэнтези, этти, BDRemux] [1080p]",
                    new[] {
                        new Lexeme(TokenType.Names, "Клинок Королевы (ТВ-3): Восстание / Queen's Blade: Rebellion"),
                        new Lexeme(TokenType.Info, "(Кагэяма Сигэнори)"),
                        new Lexeme(TokenType.AnimeType, "[TV + Special]"),
                        new Lexeme(TokenType.Series, "[12+6 из 12+6]"),
                        new Lexeme(TokenType.Info, "[без хардсаба]"),
                        new Lexeme(TokenType.Tracks, "[JAP+SUB]"),
                        new Lexeme(TokenType.Traits, "[2012 г., боевые искусства, фэнтези, этти, BDRemux]"),
                        new Lexeme(TokenType.Info, "[1080p]"),
                    });
                yield return new TestCaseData(
                    "Сильнейший в истории ученик Кеничи (Кэнити / Кэнъити) / Shijou Saikyou no Deshi Kenichi [OVA + Special] [без хардсаба] " +
                    "[5+1 из 7+1] [JAP+SUB] & [5+0 из 7+1] [RUS(ext)] [2012 г., экшн, сёнэн, боевые искусства, комедия, драма, этти, гарем, DVDRip]",
                    new[] {
                        new Lexeme(TokenType.Names, "Сильнейший в истории ученик Кеничи (Кэнити / Кэнъити) / Shijou Saikyou no Deshi Kenichi"),
                        new Lexeme(TokenType.AnimeType, "[OVA + Special]"),
                        new Lexeme(TokenType.Info, "[без хардсаба]"),
                        new Lexeme(TokenType.Series, "[5+1 из 7+1]"),
                        new Lexeme(TokenType.Tracks, "[JAP+SUB]"),
                        new Lexeme(TokenType.Info, "&"),
                        new Lexeme(TokenType.Series, "[5+0 из 7+1]"),
                        new Lexeme(TokenType.Tracks, "[RUS(ext)]"),
                        new Lexeme(TokenType.Traits, "[2012 г., экшн, сёнэн, боевые искусства, комедия, драма, этти, гарем, DVDRip]"),
                    });
                yield return new TestCaseData(
                    "Чудесные дни; Удивительные дни; Фантастические дни / Wonderful Days; Sky Blue (Moon-saeng Kim) [Movie][без хардсаба]" +
                    "[RUS(ext),KOR(int)+SUB][2003 г., Фантастика, DVDRip] Режиссерская версия / Director's Cut",
                    new[] {
                        new Lexeme(TokenType.Names, "Чудесные дни; Удивительные дни; Фантастические дни / Wonderful Days; Sky Blue"),
                        new Lexeme(TokenType.Info, "(Moon-saeng Kim)"),
                        new Lexeme(TokenType.AnimeType, "[Movie]"),
                        new Lexeme(TokenType.Info, "[без хардсаба]"),
                        new Lexeme(TokenType.Tracks, "[RUS(ext),KOR(int)+SUB]"),
                        new Lexeme(TokenType.Traits, "[2003 г., Фантастика, DVDRip]"),
                        new Lexeme(TokenType.Info, "Режиссерская версия / Director's Cut"),
                    });
                yield return new TestCaseData(
                    "Аниматрица / The Animatrix (Энди Джонс / Махиро Маеда / Ёсиаки Кавадзири) [OVA] [9 из 9] [без хардсаба] " +
                    "[RUS(int),JAP,ENG+SUB] [2003, киберпанк, фантастика, BDRip] [720p]",
                    new[] {
                        new Lexeme(TokenType.Names, "Аниматрица / The Animatrix"),
                        new Lexeme(TokenType.Info, "(Энди Джонс / Махиро Маеда / Ёсиаки Кавадзири)"),
                        new Lexeme(TokenType.AnimeType, "[OVA]"),
                        new Lexeme(TokenType.Series, "[9 из 9]"),
                        new Lexeme(TokenType.Info, "[без хардсаба]"),
                        new Lexeme(TokenType.Tracks, "[RUS(int),JAP,ENG+SUB]"),
                        new Lexeme(TokenType.Traits, "[2003, киберпанк, фантастика, BDRip]"),
                        new Lexeme(TokenType.Info, "[720p]"),
                    });
            }
        }
    }
}