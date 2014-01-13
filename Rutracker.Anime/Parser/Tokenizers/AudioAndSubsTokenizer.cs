using System;
using System.Collections.Generic;
using System.Linq;
using Rutracker.Anime.Models;

namespace Rutracker.Anime.Parser.Tokenizers
{
    public class AudioAndSubsTokenizer : TokenizerBase
    {
        public AudioAndSubsTokenizer() {}

        public AudioAndSubsTokenizer(string rx) : base(rx) {}

        public override TokenType TokenType {
            get { return TokenType.AudioAndSubs; }
        }

        public override object Tokenize(string lexeme) {
            if (string.IsNullOrWhiteSpace(lexeme))
                throw new ArgumentNullException("lexeme", "Tracks lexeme is empty");

            lexeme = RemoveBracketsIfExists(lexeme);

            return lexeme.Split(new[] { ",", ".", " " }, StringSplitOptions.RemoveEmptyEntries);
        }

        public override void UpdateModel(Models.Anime model, string lexeme) {
            var audioAndSubs = (IEnumerable<string>)Tokenize(lexeme);

            var lastUpdated = model.Video.LastOrDefault(v => v.AudioAndSubs == null);
            if (lastUpdated == null)
                model.Video.Add(new VideoContent { AudioAndSubs = audioAndSubs });
            else
                lastUpdated.AudioAndSubs = audioAndSubs;
        }
    }
}