using System;
using System.Linq;
using Rutracker.Anime.Models;

namespace Rutracker.Anime.Parser.Parts
{
    public class SeriesParser
    {
        private static readonly string[] RangeSeparators = new[] {"из", "of"};
        private static readonly string[] NumbersSeparators = new[] {"-", "+"};

        public virtual Series Parse(String part) {
            var rangeSeparator = RangeSeparators.FirstOrDefault(part.Contains);
            if (rangeSeparator == null)
                return null;

            var parts = part.Split(new[] {rangeSeparator}, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2)
                return null;

            var leftPart = parts[0].Trim();
            var rightPart = parts[1].Trim();

            int? from = null, number = null, total = null;
            if (leftPart.All(Char.IsDigit)) {
                number = int.Parse(leftPart);
            } else {
                var leftParts = SplitIntoNumbers(leftPart);
                if (leftParts == null || leftParts.Length != 2)
                    return null;

                from = leftParts[0];
                number = leftParts[1];
            }

            if (number == null)
                return null;

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
    }
}