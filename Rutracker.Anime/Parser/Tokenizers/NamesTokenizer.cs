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
            var names = new List<string> {lexeme};

            var splittedByDash = lexeme
                .Split(new[] {"/"}, StringSplitOptions.RemoveEmptyEntries)
                .Select(n => {
                    int bracketIndex;
                    var name = n;
                    while ((bracketIndex = name.IndexOf('(')) != -1 || (bracketIndex = name.IndexOf('[')) != -1)
                        name = name.Substring(0, bracketIndex);

                    return name;
                }).Select(n => n.Trim())
                .ToArray();

            names.AddRange(splittedByDash);

            var splittedBySemicolon = splittedByDash
                .Where(n => n.Contains(";"))
                .SelectMany(n => n.Split(new[] {";"}, StringSplitOptions.RemoveEmptyEntries))
                .ToArray();

            names.AddRange(splittedBySemicolon);
            
            return names;
        }
    }
}