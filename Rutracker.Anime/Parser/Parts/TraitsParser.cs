using System;
using System.Collections.Generic;
using System.Linq;
using Rutracker.Anime.Models;

namespace Rutracker.Anime.Parser.Parts
{
    public class TraitsParser
    {
        public static Traits GetTraits(String value) {
            var year = GetFirstYear(value);
            if (year == null)
                return null;

            var commaSeparated = value.Replace('.', ',').Replace(",,", ",");
            var blocks = TrimEach(commaSeparated.Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries));

            var genres = GetGenres(blocks);
            if (genres == null)
                return null;

            var format = blocks[blocks.Length - 1];

            return new Traits(year, genres, format);
        }

        private static List<string> GetGenres(IList<string> blocks) {
            var genres = new List<string>();
            if (blocks.Count < 3)
                return null;

            if (blocks.Count == 3)
                genres.Add(blocks[1]);
            else
                genres.AddRange(blocks.Skip(1));

            return genres;
        }

        private static string[] TrimEach(IList<string> blocks) {
            var result = new string[blocks.Count];
            for (var i = 0; i < blocks.Count; i++)
                result[i] = blocks[i].Trim();

            return result;
        }

        private static int? GetFirstYear(string value) {
            for (var i = 0; i < 4; i++)
                if (!Char.IsDigit(value[i]))
                    return null;

            var strYear = value.Substring(0, 4);
            return int.Parse(strYear);
        }
    }
}