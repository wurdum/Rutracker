namespace Rutracker.Anime.Parser.Tokenizers
{
    public interface ILexemeEvaluator {
        TokenType TokenType { get; }
        bool IsSatisfy(string lexeme);
    }
}