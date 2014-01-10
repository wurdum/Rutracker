using System.Collections.Generic;
using NUnit.Framework;
using Rutracker.Anime.Exceptions;
using Rutracker.Anime.Parser.Tokenizers;

namespace Rutracker.Anime.Tests.Tokenizers
{
    [TestFixture]
    public class NamesTokenizerTests
    {
        private readonly NamesTokenizer _namesTokenizer = new NamesTokenizer();

        [Test, TestCaseSource("MainTestCases")]
        public void MainTest(string lexeme, IEnumerable<string> expected) {
            if (!_namesTokenizer.IsSatisfy(lexeme))
                throw new TokenizerException("Lexeme not satisfies tokenizer", lexeme, TokenType.Names);

            var actual = (IEnumerable<string>)_namesTokenizer.Tokenize(lexeme);

            CollectionAssert.AreEquivalent(expected, actual);
        }

        private static IEnumerable<TestCaseData> MainTestCases {
            get {
                yield return new TestCaseData("Ююшики: Няняшики / Yuyushiki: Nyanyashiki", new[] { "Ююшики: Няняшики", "Yuyushiki: Nyanyashiki" });
                yield return new TestCaseData("Одному лишь Богу ведомый мир (ТВ-3) / Kami nomi zo Shiru Sekai: Megami Hen", new[] { "Одному лишь Богу ведомый мир", "Kami nomi zo Shiru Sekai: Megami Hen" });
                yield return new TestCaseData("КсамД: Позабывший невзгоды / Xam'd: Lost Memories", new[] { "КсамД: Позабывший невзгоды", "Xam'd: Lost Memories" });
                yield return new TestCaseData("Кланнад [ТВ-2]. Продолжение истории / Clannad After Story", new[] { "Кланнад. Продолжение истории", "Clannad After Story" });
                yield return new TestCaseData("Аниматрица / The Animatrix", new[] { "Аниматрица", "The Animatrix" });
                yield return new TestCaseData("Кэйон! (фильм) / Eiga K-On! / K-ON! Movie", new[] { "Кэйон!", "Eiga K-On!", "K-ON! Movie" });
                yield return new TestCaseData("Some name (with) 1 / Some name 2", new[] { "Some name 1", "Some name 2" });
            }
        }
    }
}