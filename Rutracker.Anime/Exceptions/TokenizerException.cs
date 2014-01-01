using System;
using Rutracker.Anime.Parser;

namespace Rutracker.Anime.Exceptions
{
    public class TokenizerException : Exception
    {
        public TokenizerException(string message) : base(message) {}
        public TokenizerException(string message, PartTypePattern.PartType tokenizerType) : base(message) {
            TokenizerType = tokenizerType;
        }
        public TokenizerException(string message, string lexeme, PartTypePattern.PartType tokenizerType) : base(message) {
            Lexeme = lexeme;
            TokenizerType = tokenizerType;
        }

        public string Lexeme { get; private set; }
        public PartTypePattern.PartType TokenizerType { get; private set; }
    }
}