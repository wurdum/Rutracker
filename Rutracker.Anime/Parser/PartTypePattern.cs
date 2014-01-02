using System;
using System.Text.RegularExpressions;
using Rutracker.Anime.Parser.Tokenizers;

namespace Rutracker.Anime.Parser
{
    public class PartTypePattern
    {
        private readonly Regex _regex;
        private readonly TokenType _tokenType;

        public PartTypePattern(String regex, TokenType tokenType) {
            _tokenType = tokenType;
            _regex = new Regex(regex, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        public TokenType getPartType() {
            return _tokenType;
        }

        public bool isSatisfy(String value) {
            return _regex.IsMatch(value);
        }
    }
}