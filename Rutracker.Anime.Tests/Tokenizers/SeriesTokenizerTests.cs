using System.Collections.Generic;
using NUnit.Framework;
using Rutracker.Anime.Exceptions;
using Rutracker.Anime.Models;
using Rutracker.Anime.Parser.Tokenizers;

namespace Rutracker.Anime.Tests.Tokenizers
{
    [TestFixture]
    public class SeriesTokenizerTests
    {
        private readonly SeriesTokenizer _seriesTokenizer = new SeriesTokenizer();

        [Test, TestCaseSource("MainTestCases")]
        public void MainTest(string lexeme, Series expected) {
            if (!_seriesTokenizer.IsSatisfy(lexeme))
                throw new TokenizerException("Lexeme not satisfies tokenizer", lexeme, _seriesTokenizer.TokenType);

            var actual = (Series)_seriesTokenizer.Tokenize(lexeme);

            Assert.AreEqual(expected, actual);
        }

        private static IEnumerable<TestCaseData> MainTestCases {
            get {
                yield return new TestCaseData("[12+6 из 12+6]", new Series(12, 12));
                yield return new TestCaseData("[51 из 51]", new Series(51, 51));
                yield return new TestCaseData("[08 из 24]", new Series(8, 24));
                yield return new TestCaseData("[01-12 из 12]", new Series(1, 12, 12));
                yield return new TestCaseData("[10-12 из 12]", new Series(10, 12, 12));
                yield return new TestCaseData("[13+3 из 13+3]", new Series(1, 13, 13));
                yield return new TestCaseData("[1 из >1]", new Series(1, 1, null));
                yield return new TestCaseData("[6 из >12]", new Series(1, 6, null));
            }
        }
    }
}