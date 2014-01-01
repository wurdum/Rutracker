using System.Text.RegularExpressions;

namespace Rutracker.Anime.Parser.Parts
{
    public abstract class TokenizerBase
    {
        public abstract PartTypePattern.PartType TokenType { get; }
        public Regex TokenRx { get; protected set; }

        public abstract object Tokenize(string lexeme);

        public virtual bool IsSatisfy(string lexeme) {
            return TokenRx.IsMatch(lexeme);
        }

        protected virtual string RemoveBracketsIfExists(string lexeme) {
            return lexeme.Trim('[', '(', ']', ')');
        }
    }
}