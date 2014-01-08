using System;
using System.Collections.Generic;
using System.Linq;
using Rutracker.Anime.Exceptions;
using Rutracker.Anime.Models;

namespace Rutracker.Anime.Parser.Tokenizers
{
    public class TraitsTokenizer : TokenizerBase
    {
        public TraitsTokenizer() {}

        public TraitsTokenizer(string rx) : base(rx) {}

        public override TokenType TokenType {
            get { return TokenType.Traits; }
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

        public override void UpdateModel(Models.Anime model, string lexeme) {
            model.Traits = (Traits)Tokenize(lexeme);
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