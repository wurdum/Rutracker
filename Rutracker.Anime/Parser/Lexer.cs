using System.Collections.Generic;
using System.Linq;
using Rutracker.Anime.Exceptions;
using Rutracker.Anime.Parser.Tokenizers;

namespace Rutracker.Anime.Parser
{
    public class Lexer
    {
        private readonly Scanner _scanner;
        private readonly IEnumerable<ITokenizer> _tokenizers;

        public Lexer(Scanner scanner, IEnumerable<ITokenizer> tokenizers) {
            _scanner = scanner;
            _tokenizers = tokenizers;
        }

        public Models.Anime Parse(string title) {
            var anime = new Models.Anime {Title = title};

            var lexemes = _scanner.Scan(title);
            foreach (var lexeme in lexemes) {
                var tokenizer = _tokenizers.FirstOrDefault(t => t.TokenType == lexeme.Type);
                if (tokenizer == null)
                    throw new TokenizerException("No tokenizer found for lexeme", lexeme.String, lexeme.Type);

                tokenizer.UpdateModel(anime, lexeme.String);
            }

            return anime;
        }
    }
}