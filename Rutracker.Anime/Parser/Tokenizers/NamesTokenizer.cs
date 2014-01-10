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

            var splittedByDash = lexeme
                .Split(new[] {"/"}, StringSplitOptions.RemoveEmptyEntries)
                .Select(n => {
                    var name = n.Trim();
                    var firstBracketIndex = -1;
                    var firstBracketIndexes = new[] {name.IndexOf('('), name.IndexOf('[')}
                        .Where(i => i != -1).ToArray();
                    if (firstBracketIndexes.Any())
                        firstBracketIndex = firstBracketIndexes.Min();

                    var lastBracketIndex = new[] {name.LastIndexOf(')'), name.LastIndexOf(']')}.Max();

                    if (firstBracketIndex != -1 && Char.IsWhiteSpace(name[firstBracketIndex - 1]))
                        firstBracketIndex -= 1;

                    if (firstBracketIndex == -1 && lastBracketIndex == -1)
                        return name;

                    var nameWithoutBrackets = name.Substring(0, firstBracketIndex) +
                        name.Substring(lastBracketIndex + 1, name.Length - lastBracketIndex - 1);

                    return nameWithoutBrackets;
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

        public override void UpdateModel(Models.Anime model, string lexeme) {
            model.Names = (IEnumerable<string>)Tokenize(lexeme);
        }
    }
}