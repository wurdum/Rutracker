using System.Collections.Generic;
using NUnit.Framework;
using Rutracker.Anime.Exceptions;
using Rutracker.Anime.Parser.Tokenizers;

namespace Rutracker.Anime.Tests.Tokenizers
{
    [TestFixture]
    public class TypeTokenizerTests
    {
        private readonly TypesTokenizer _typesTokenizer = new TypesTokenizer();

        [Test, TestCaseSource("MainTestCases")]
        public void MainTest(string lexeme, IEnumerable<Models.Anime.Type> expected) {
            if (!_typesTokenizer.IsSatisfy(lexeme))
                throw new TokenizerException("Lexeme not satisfies tokenizer", lexeme, _typesTokenizer.TokenType);

            var actual = (IEnumerable<Models.Anime.Type>)_typesTokenizer.Tokenize(lexeme);

            CollectionAssert.AreEquivalent(actual, expected);
        }

        private static IEnumerable<TestCaseData> MainTestCases {
            get {
                yield return new TestCaseData("[TV]", new[] { Models.Anime.Type.TV });
                yield return new TestCaseData("[tv]", new[] { Models.Anime.Type.TV });
                yield return new TestCaseData("[Movie]", new[] { Models.Anime.Type.Movie });
                yield return new TestCaseData("[Special]", new[] { Models.Anime.Type.Special });
                yield return new TestCaseData("[Specials]", new[] { Models.Anime.Type.Special });
                yield return new TestCaseData("[TV+Specials]", new[] { Models.Anime.Type.TV, Models.Anime.Type.Special });
                yield return new TestCaseData("[TV+movie]", new[] { Models.Anime.Type.TV, Models.Anime.Type.Movie });
                yield return new TestCaseData("[ova+tv+movie]", new[] { Models.Anime.Type.TV, Models.Anime.Type.Movie, Models.Anime.Type.OVA });
            }
        }
    }
}