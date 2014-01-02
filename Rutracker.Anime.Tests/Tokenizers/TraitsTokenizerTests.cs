using System.Collections.Generic;
using NUnit.Framework;
using Rutracker.Anime.Exceptions;
using Rutracker.Anime.Models;
using Rutracker.Anime.Parser.Tokenizers;

namespace Rutracker.Anime.Tests.Tokenizers
{
    [TestFixture]
    public class TraitsTokenizerTests
    {
        private readonly TraitsTokenizer _traitsTokenizer = new TraitsTokenizer();

        [Test, TestCaseSource("MainTestCases")]
        public void MainTest(string lexeme, Traits expected) {
            if (!_traitsTokenizer.IsSatisfy(lexeme))
                throw new TokenizerException("Lexeme not satisfies tokenizer");

            var actual = (Traits)_traitsTokenizer.Tokenize(lexeme);

            Assert.AreEqual(expected, actual);
        }

        private static IEnumerable<TestCaseData> MainTestCases {
            get {
                yield return new TestCaseData("[2011 г., Комедия, Повседневность, Школа, BDRip]", new Traits(2011, new[] { "Комедия", "Повседневность", "Школа" }, "BDRip"));
                yield return new TestCaseData("[2009 г., вампиры, ужасы, триллер, BDRemux]", new Traits(2009, new[] { "вампиры", "ужасы", "триллер" }, "BDRemux"));
                yield return new TestCaseData("[2009 г., самурайский боевик, BDRip]", new Traits(2009, new[] { "самурайский боевик" }, "BDRip"));
                yield return new TestCaseData("[1992-1998 гг., приключения, фантастика, меха, BDRip]", new Traits(1992, new[] { "приключения", "фантастика", "меха" }, "BDRip"));
                yield return new TestCaseData("[2009 г, комедия, HDTVRip]", new Traits(2009, new[] { "комедия" }, "HDTVRip"));
                yield return new TestCaseData("[2009, вампиры, ужасы, триллер, BDRemux]", new Traits(2009, new[] { "вампиры", "ужасы", "триллер" }, "BDRemux"));
                yield return new TestCaseData("[2009 г, комедия]", null).Throws(typeof(TokenizerException));
            }
        }
    }
}