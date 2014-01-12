using System.Collections.Generic;
using NUnit.Framework;
using Rutracker.Anime.Exceptions;
using Rutracker.Anime.Parser.Tokenizers;

namespace Rutracker.Anime.Tests.Tokenizers
{
    [TestFixture]
    public class TracksTokenizerTests
    {
        private readonly AudioAndSubsTokenizer _audioAndSubsTokenizer = new AudioAndSubsTokenizer();

        [Test, TestCaseSource("MainTestCases")]
        public void MainTest(string lexeme, IEnumerable<string> expected) {
            if (!_audioAndSubsTokenizer.IsSatisfy(lexeme))
                throw new TokenizerException("Lexeme not satisfies tokenizer", lexeme, _audioAndSubsTokenizer.TokenType);

            var actual = (IEnumerable<string>)_audioAndSubsTokenizer.Tokenize(lexeme);

            CollectionAssert.AreEquivalent(expected, actual);
        }

        private static IEnumerable<TestCaseData> MainTestCases {
            get {
                yield return new TestCaseData("[JAP+SUB]", new[] { "JAP+SUB" });
                yield return new TestCaseData("[RUS(int), JAP+SUB]", new[] { "RUS(int)", "JAP+SUB" });
                yield return new TestCaseData("[RUS(ext),JAP+SUB]", new[] { "RUS(ext)", "JAP+SUB" });
                yield return new TestCaseData("[RUS, JAP]", new[] { "RUS", "JAP" });
                yield return new TestCaseData("[RUS JAP]", new[] { "RUS", "JAP" });
            }
        }
    }
}