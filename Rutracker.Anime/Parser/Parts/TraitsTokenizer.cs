using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using Rutracker.Anime.Exceptions;
using Rutracker.Anime.Models;

namespace Rutracker.Anime.Parser.Parts
{
    public class TraitsTokenizer : TokenizerBase
    {
        public TraitsTokenizer() {
            TokenRx = new Regex(ConfigurationManager.AppSettings[TokenType.ToString()]);
        }

        public TraitsTokenizer(string rxString) {
            TokenRx = new Regex(rxString);
        }

        public override PartTypePattern.PartType TokenType {
            get { return PartTypePattern.PartType.Traits; }
        }

        public override object Tokenize(string lexeme) {
            if (string.IsNullOrWhiteSpace(lexeme))
                throw new ArgumentNullException("lexeme", "Traits lexeme is epmty");

            lexeme = RemoveBracketsIfExists(lexeme);

            var year = GetFirstYear(lexeme);
            if (year == null)
                throw new TokenizerException("Year was not found in lexeme", lexeme, TokenType);

            var commaSeparated = lexeme.Replace('.', ',').Replace(",,", ",");
            var blocks = commaSeparated.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim()).ToArray();

            var genres = GetGenres(blocks);
            if (genres == null)
                throw new TokenizerException("Genres was not found in lexeme", lexeme, TokenType);

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