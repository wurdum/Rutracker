namespace Rutracker.Anime.Parser.Tokenizers
{
    public interface ITokenizer {
        TokenType TokenType { get; }
        object Tokenize(string lexeme);
        void UpdateModel(Models.Anime model, string lexeme);
    }
}