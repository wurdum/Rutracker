using System;
using System.Configuration;
using System.Text.RegularExpressions;

namespace Rutracker.Anime.Parser.Parts
{
    public class TracksTokenizer : TokenizerBase
    {
        public TracksTokenizer() {
            TokenRx = new Regex(ConfigurationManager.AppSettings[TokenType.ToString()]);
        }

        public TracksTokenizer(string rxString) {
            TokenRx = new Regex(rxString);
        }

        public override object Tokenize(string lexeme) {
            if (string.IsNullOrWhiteSpace(lexeme))
                throw new ArgumentNullException("lexeme", "Tracks lexeme is empty");

            lexeme = RemoveBracketsIfExists(lexeme);

            return lexeme.Split(new[] { ",", "+", " " }, StringSplitOptions.RemoveEmptyEntries);
        }

        public override PartTypePattern.PartType TokenType {
            get { return PartTypePattern.PartType.AvailTracks; }
        }
    }
}