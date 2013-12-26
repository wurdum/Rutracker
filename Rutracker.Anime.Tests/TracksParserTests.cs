﻿using System.Collections.Generic;
using NUnit.Framework;
using Rutracker.Anime.Parser.Parts;

namespace Rutracker.Anime.Tests
{
    [TestFixture]
    public class TracksParserTests
    {
        private readonly TracksParser _tracksParser = new TracksParser();

        [Test, TestCaseSource("MainTestCases")]
        public void MainTest(string part, IEnumerable<string> expected) {
            var actual = _tracksParser.Parse(part);

            CollectionAssert.AreEquivalent(expected, actual);
        }

        private static IEnumerable<TestCaseData> MainTestCases {
            get {
                yield return new TestCaseData("JAP+SUB", new[] { "JAP", "SUB" });
                yield return new TestCaseData("RUS(int), JAP+SUB", new[] { "RUS(int)", "JAP", "SUB" });
                yield return new TestCaseData("RUS(ext),JAP+SUB", new[] { "RUS(ext)", "JAP", "SUB" });
                yield return new TestCaseData("RUS, JAP", new[] { "RUS", "JAP" });
                yield return new TestCaseData("RUS JAP", new[] { "RUS", "JAP" });
            }
        }
    }
}