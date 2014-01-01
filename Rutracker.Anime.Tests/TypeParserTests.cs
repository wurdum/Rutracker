using System.Collections.Generic;
using NUnit.Framework;
using Rutracker.Anime.Parser.Parts;
using AnimeType = Rutracker.Anime.Models.Anime.Type;

namespace Rutracker.Anime.Tests
{
    [TestFixture]
    public class TypeParserTests
    {
        private readonly TypesParser _typesParser = new TypesParser();

        [Test, TestCaseSource("MainTestCases")]
        public void MainTest(string part, IEnumerable<AnimeType> expected) {
            var actual = _typesParser.Parse(part);

            CollectionAssert.AreEquivalent(actual, expected);
        }

        private static IEnumerable<TestCaseData> MainTestCases {
            get {
                yield return new TestCaseData("[TV]", new[] { AnimeType.TV });
                yield return new TestCaseData("[tv]", new[] { AnimeType.TV });
                yield return new TestCaseData("[Movie]", new[] { AnimeType.Movie });
                yield return new TestCaseData("[Special]", new[] { AnimeType.Special });
                yield return new TestCaseData("[Specials]", new[] { AnimeType.Special });
                yield return new TestCaseData("[TV+Specials]", new[] { AnimeType.TV, AnimeType.Special });
                yield return new TestCaseData("[TV+movie]", new[] { AnimeType.TV, AnimeType.Movie });
                yield return new TestCaseData("[ova+tv+movie]", new[] { AnimeType.TV, AnimeType.Movie, AnimeType.OVA });
            }
        }
    }
}