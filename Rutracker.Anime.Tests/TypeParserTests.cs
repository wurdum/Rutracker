using System.Collections.Generic;
using NUnit.Framework;
using Rutracker.Anime.Parser.Parts;

namespace Rutracker.Anime.Tests
{
    [TestFixture]
    public class TypeParserTests
    {
        private readonly TypesParser _typesParser = new TypesParser();

        [Test, TestCaseSource("MainTestCases")]
        public void MainTest(string part, IEnumerable<Models.Anime.Type> expected) {
            var actual = _typesParser.Parse(part);

            CollectionAssert.AreEquivalent(actual, expected);
        }

        private static IEnumerable<TestCaseData> MainTestCases {
            get {
                yield return new TestCaseData("TV", new[] { Models.Anime.Type.TV });
                yield return new TestCaseData("tv", new[] { Models.Anime.Type.TV });
                yield return new TestCaseData("Movie", new[] { Models.Anime.Type.Movie });
                yield return new TestCaseData("Special", new[] { Models.Anime.Type.Special });
                yield return new TestCaseData("Specials", new[] { Models.Anime.Type.Special });
                yield return new TestCaseData("TV+Specials", new[] { Models.Anime.Type.TV, Models.Anime.Type.Special });
                yield return new TestCaseData("TV+movie", new[] { Models.Anime.Type.TV, Models.Anime.Type.Movie });
                yield return new TestCaseData("ova+tv+movie", new[] { Models.Anime.Type.TV, Models.Anime.Type.Movie, Models.Anime.Type.OVA });
            }
        }
    }
}