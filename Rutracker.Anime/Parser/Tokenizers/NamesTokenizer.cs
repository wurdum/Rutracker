using System;
using System.Collections.Generic;
using System.Linq;

namespace Rutracker.Anime.Parser.Tokenizers
{
    public class NamesTokenizer : TokenizerBase
    {
        public NamesTokenizer() {}

        public NamesTokenizer(string rx) : base(rx) {}

        public override TokenType TokenType {
            get { return TokenType.Names; }
        }

        public override object Tokenize(string lexeme) {
            var names = new List<string>();

            lexeme = RemoveTextInBrackets(lexeme);
            var splittedByDash = lexeme
                .Split(new[] {"/"}, StringSplitOptions.RemoveEmptyEntries)
                .Select(n => n.Trim())
                .ToArray();

            names.AddRange(splittedByDash);

            var splittedBySemicolon = splittedByDash
                .Where(n => n.Contains(";"))
                .SelectMany(n => n.Split(new[] {";"}, StringSplitOptions.RemoveEmptyEntries))
                .ToArray();

            names.AddRange(splittedBySemicolon);
            
            return names;
        }

        private string RemoveTextInBrackets(string str) {
            int open = -1, close = -1;
            while (((open = str.IndexOf('(')) != -1 && (close = str.IndexOf(')', open)) != -1) ||
                   ((open = str.IndexOf('[')) != -1 && (close = str.IndexOf(']', open)) != -1)) {

                if (open == 0) {
                    str = str.Substring(close + 1);
                    continue;
                }

                if (Char.IsWhiteSpace(str[open - 1]))
                    open -= 1;

                str = str.Substring(0, open) + str.Substring(close + 1, str.Length - close - 1);
            }

            return str;
        }

        public override void UpdateModel(Models.Anime model, string lexeme) {
            model.Names = (IEnumerable<string>)Tokenize(lexeme);
        }
    }
}