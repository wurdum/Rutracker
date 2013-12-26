using System;
using System.Collections.Generic;
using System.Linq;
using Rutracker.Anime.Models;

namespace Rutracker.Anime.Parser.Parts
{
    public class TraitsParser
    {
        public Traits Parse(String value) {
            var year = GetFirstYear(value);
            if (year == null)
                return null;

            var commaSeparated = value.Replace('.', ',').Replace(",,", ",");
            var blocks = commaSeparated.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim()).ToArray();

            var genres = GetGenres(blocks);
            if (genres == null)
                return null;

            var format = blocks[blocks.Length - 1];

            return new Traits(year, genres, format);
        }

        private IEnumerable<string> GetGenres(string[] blocks) {
            var genres = new List<string>();
            if (blocks.Length < 3)
                return null;

            if (blocks.Length == 3)
                genres.Add(blocks[1]);
            else
                genres.AddRange(blocks.Skip(1).Take(blocks.Length - 2));

            return genres;
        }

        private int? GetFirstYear(string value) {
            if (!value.Take(4).All(Char.IsDigit))
                return null;

            var strYear = value.Substring(0, 4);
            return int.Parse(strYear);
        }
    }
}