namespace Rutracker.Anime.Parser.Tokenizers
{
    public interface ITokenizer {
        object Tokenize(string lexeme);
    }
}