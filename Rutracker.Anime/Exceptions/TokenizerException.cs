using System;
using Rutracker.Anime.Parser;
using Rutracker.Anime.Parser.Tokenizers;

namespace Rutracker.Anime.Exceptions
{
    public class TokenizerException : Exception
    {
        public TokenizerException(string message) : base(message) {}
        public TokenizerException(string message, TokenType tokenizerType) : base(message) {
            TokenizerType = tokenizerType;
        }
        public TokenizerException(string message, string lexeme, TokenType tokenizerType) : base(message) {
            Lexeme = lexeme;
            TokenizerType = tokenizerType;
        }

        public string Lexeme { get; private set; }
        public TokenType TokenizerType { get; private set; }
    }
}