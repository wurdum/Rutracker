using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;

namespace Rutracker.Anime.Parser.Tokenizers
{
    public interface ITokenHandler
    {
        TokenType TokenType { get; }
    }

    public interface ITokenEvaluator : ITokenHandler
    {
        bool IsSatisfy(string lexeme);
    }

    public interface ITokenizer : ITokenHandler
    {
        object Tokenize(string lexeme);
        void UpdateModel(Models.Anime model, string lexeme);
    }

    public enum TokenType
    {
        Names = 1,
        Traits,
        Series,
        AudioAndSubs,
        AnimeType,
        Info
    }

    public abstract class TokenizerBase : ITokenEvaluator, ITokenizer
    {
        private static readonly char[] Brackets = new[] {'[', '(', ']', ')'};

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
            if (Brackets.Contains(lexeme[0]) && Brackets.Contains(lexeme[lexeme.Length - 1]))
                return lexeme.Substring(1, lexeme.Length - 2);

            return lexeme;
        }
    }
}