using System.Configuration;
using System.Text.RegularExpressions;

namespace Rutracker.Anime.Parser.Tokenizers
{
    public abstract class TokenizerBase
    {
        protected TokenizerBase() {
            TokenRx = new Regex(ConfigurationManager.AppSettings[TokenType.ToString()], RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        protected TokenizerBase(string rx) {
            TokenRx = new Regex(rx, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        protected Regex TokenRx { get; set; }
        public abstract TokenType TokenType { get; }

        public abstract object Tokenize(string lexeme);
        public abstract void UpdateModel(Models.Anime model, string lexeme);

        public virtual bool IsSatisfy(string lexeme) {
            return TokenRx.IsMatch(lexeme);
        }

        protected virtual string RemoveBracketsIfExists(string lexeme) {
            return lexeme.Trim('[', '(', ']', ')');
        }
    }
}