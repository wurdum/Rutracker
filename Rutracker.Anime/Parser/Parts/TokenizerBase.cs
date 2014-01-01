namespace Rutracker.Anime.Parser.Parts
{
    public abstract class TokenizerBase
    {
        public abstract PartTypePattern.PartType TokenType { get; }

        public abstract object Tokenize(string lexeme);

        public virtual bool IsSatisfy() {
            return true;
        }

        protected virtual string RemoveBracketsIfExists(string lexeme) {
            return lexeme.Trim('[', '(', ']', ')');
        }
    }
}