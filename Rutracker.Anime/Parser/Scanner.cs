using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Rutracker.Anime.Exceptions;
using Rutracker.Anime.Parser.Tokenizers;

namespace Rutracker.Anime.Parser
{
    public class Scanner
    {
        private readonly Regex _scannerRx = new Regex(@"
        (
            (
                [^\[\(\)\]]+|                           # any text block without brackets inside
                (
                    \[[^\]]+\]|
                    \([^\(]+\)
                )
                ([^\[\(\)\]]*)?\/(?=.*[\[\]\(\)]))+|    # any block in brackets if '/' comming next like '(ТВ) / '
                \[[^\]]+\]|                             # any block in square bracket
                \([^\(]+\)                              # any block in parenthetical 
        )", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
        private readonly IEnumerable<ILexemeEvaluator> _evaluators;

        public Scanner(IEnumerable<ILexemeEvaluator> evaluators) {
            _evaluators = evaluators;
        }

        public IEnumerable<Lexeme> Scan(string title) {
            var groups = _scannerRx.Matches(title);
            if (groups.Count == 0)
                throw new ScannerException("No lexems found in title", title);

            var succeded = groups.Cast<Group>().Where(n => n.Success && !string.IsNullOrWhiteSpace(n.Value)).ToArray();
            var lexemes = new List<Lexeme>(succeded.Length);
            foreach (var g in succeded) {
                var lexeme = g.Value.Trim();
                var evaluator = _evaluators.FirstOrDefault(e => e.IsSatisfy(lexeme));
                if (evaluator == null)
                    throw new ScannerException("No one evaluator succeded on lexeme", lexeme);

                var type = evaluator.TokenType;
                if (type == TokenType.Names && lexemes.Any(l => l.Type == TokenType.Names))
                    type = TokenType.Info;

                lexemes.Add(new Lexeme(type, lexeme));
            }

            return lexemes;
        }
    }

    public struct Lexeme
    {
        public readonly TokenType Type;
        public readonly string String;

        public Lexeme(TokenType type, string s) {
            Type = type;
            String = s;
        }

        public bool Equals(Lexeme other) {
            return Type == other.Type && string.Equals(String, other.String);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj))
                return false;
            return obj is Lexeme && Equals((Lexeme) obj);
        }

        public override int GetHashCode() {
            unchecked {
                return ((int) Type*397) ^ (String != null ? String.GetHashCode() : 0);
            }
        }

        public override string ToString() {
            return string.Format("Type: {0}, S: {1}", Type, String);
        }
    }
}