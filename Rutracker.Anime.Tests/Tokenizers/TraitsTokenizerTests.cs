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
                yield return new TestCaseData("[2011 �., �������, ��������������, �����, BDRip]", new Traits(2011, new[] { "�������", "��������������", "�����" }, "BDRip"));
                yield return new TestCaseData("[2009 �., �������, �����, �������, BDRemux]", new Traits(2009, new[] { "�������", "�����", "�������" }, "BDRemux"));
                yield return new TestCaseData("[2009 �., ����������� ������, BDRip]", new Traits(2009, new[] { "����������� ������" }, "BDRip"));
                yield return new TestCaseData("[1992-1998 ��., �����������, ����������, ����, BDRip]", new Traits(1992, new[] { "�����������", "����������", "����" }, "BDRip"));
                yield return new TestCaseData("[2009 �, �������, HDTVRip]", new Traits(2009, new[] { "�������" }, "HDTVRip"));
                yield return new TestCaseData("[2009, �������, �����, �������, BDRemux]", new Traits(2009, new[] { "�������", "�����", "�������" }, "BDRemux"));
                yield return new TestCaseData("[2009 �, �������]", null).Throws(typeof(TokenizerException));
            }
        }
    }
}