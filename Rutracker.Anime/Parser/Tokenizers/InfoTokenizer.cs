﻿namespace Rutracker.Anime.Parser.Tokenizers
{
    public class InfoTokenizer : TokenizerBase
    {
        public InfoTokenizer() : base(".*") {}
        public InfoTokenizer(string rx) : base(rx) {}

        public override TokenType TokenType {
            get { return TokenType.Info; }
        }

        public override object Tokenize(string lexeme) {
            return lexeme;
        }
    }
}