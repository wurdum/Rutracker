using System;
using System.Collections.Generic;
using Rutracker.Anime.Models;

namespace Rutracker.Anime.Parser.Parts
{
    public class SeriesParser
    {
        private static readonly List<string> RangeSeparators = new List<string> {"из", "of"};
        private static readonly List<string> NumbersSeparators = new List<string> {"-", "+"};

        public static Series GetSeries(String value) {
            var parts = new String[0];

            foreach (var separator in RangeSeparators)
                if (value.Contains(separator)) {
                    parts = value.Split(new[] {separator}, StringSplitOptions.RemoveEmptyEntries);
                    break;
                }

            if (parts.Length != 2)
                return null;

            int? from = null, number = null, total = null;
            var leftPart = parts[0].Trim();
            var rightPart = parts[1].Trim();

            if (AllAreDigits(leftPart)) {
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

            if (AllAreDigits(rightPart)) {
                total = int.Parse(rightPart);
            } else {
                var rightParts = SplitIntoNumbers(rightPart);
                if (rightParts != null && rightParts.Length == 2)
                    total = rightParts[0] ?? rightParts[1];
            }

            return from == null ? new Series(number, total) : new Series(from, number, total);
        }

        private static int?[] SplitIntoNumbers(String part) {
            foreach (var ns in NumbersSeparators) {
                if (!part.Contains(ns))
                    continue;

                var startingParts = part.Split(new[] {ns}, StringSplitOptions.RemoveEmptyEntries);
                if (startingParts.Length != 2)
                    return null;

                var stPart1 = startingParts[0].Trim();
                var stPart2 = startingParts[1].Trim();
                if (!AllAreDigits(stPart1) || !AllAreDigits(stPart2))
                    return null;

                if (ns.Equals("+"))
                    return new int?[] {null, int.Parse(stPart1)};

                return new int?[] {int.Parse(stPart1), int.Parse(stPart2)};
            }

            return null;
        }

        private static bool AllAreDigits(String value) {
            var length = value.Length;
            for (var i = 0; i < length; i++)
                if (!Char.IsDigit(value[i]))
                    return false;

            return true;
        }
    }
}