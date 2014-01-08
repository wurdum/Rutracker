using System;
using System.Collections.Generic;

namespace Rutracker.Anime.Parser.Tokenizers
{
    public class TracksTokenizer : TokenizerBase
    {
        public TracksTokenizer() {}

        public TracksTokenizer(string rx) : base(rx) {}

        public override TokenType TokenType {
            get { return TokenType.Tracks; }
        }

        public override object Tokenize(string lexeme) {
            if (string.IsNullOrWhiteSpace(lexeme))
                throw new ArgumentNullException("lexeme", "Tracks lexeme is empty");

            lexeme = RemoveBracketsIfExists(lexeme);

            return lexeme.Split(new[] { ",", ".", " " }, StringSplitOptions.RemoveEmptyEntries);
        }

        public override void UpdateModel(Models.Anime model, string lexeme) {
            model.Tracks = (IEnumerable<string>)Tokenize(lexeme);
        }
    }
}