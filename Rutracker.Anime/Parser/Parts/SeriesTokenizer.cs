using System;
using System.Linq;
using Rutracker.Anime.Exceptions;
using Rutracker.Anime.Models;

namespace Rutracker.Anime.Parser.Parts
{
    public class SeriesTokenizer : TokenizerBase
    {
        private static readonly string[] RangeSeparators = new[] {"из", "of"};
        private static readonly string[] NumbersSeparators = new[] {"-", "+"};

        public override object Tokenize(String lexeme) {
            if (string.IsNullOrWhiteSpace(lexeme))
                throw new ArgumentNullException("lexeme", "Series lexem is empty");

            lexeme = RemoveBracketsIfExists(lexeme);

            var rangeSeparator = RangeSeparators.FirstOrDefault(lexeme.Contains);
            if (rangeSeparator == null)
                throw new TokenizerException("No range separator", lexeme, TokenType);

            var parts = lexeme.Split(new[] {rangeSeparator}, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2)
                throw new TokenizerException("No 2 parts in lexeme", lexeme, TokenType);

            var leftPart = parts[0].Trim();
            var rightPart = parts[1].Trim();

            int? from = null, number = null, total = null;
            if (leftPart.All(Char.IsDigit)) {
                number = int.Parse(leftPart);
            } else {
                var leftParts = SplitIntoNumbers(leftPart);
                if (leftParts == null || leftParts.Length != 2)
                    throw new TokenizerException("Left part of lexeme is unrecognizable", lexeme, TokenType);

                from = leftParts[0];
                number = leftParts[1];
            }

            if (number == null)
                throw new TokenizerException("Number of series not found", lexeme, TokenType);

            if (rightPart.All(Char.IsDigit)) {
                total = int.Parse(rightPart);
            } else {
                var rightParts = SplitIntoNumbers(rightPart);
                if (rightParts != null && rightParts.Length == 2)
                    total = rightParts[0] ?? rightParts[1];
            }

            return from == null ? new Series(number.Value, total) : new Series(from, number, total);
        }

        private int?[] SplitIntoNumbers(String part) {
            foreach (var ns in NumbersSeparators) {
                if (!part.Contains(ns))
                    continue;

                var startingParts = part.Split(new[] {ns}, StringSplitOptions.RemoveEmptyEntries);
                if (startingParts.Length != 2)
                    return null;

                var stPart1 = startingParts[0].Trim();
                var stPart2 = startingParts[1].Trim();
                if (!stPart1.All(Char.IsDigit) || !stPart2.All(Char.IsDigit))
                    return null;

                return ns.Equals("+") 
                    ? new int?[] {null, int.Parse(stPart1)} 
                    : new int?[] {int.Parse(stPart1), int.Parse(stPart2)};
            }

            return null;
        }

        public override PartTypePattern.PartType TokenType {
            get { return PartTypePattern.PartType.Series; }
        }
    }
}