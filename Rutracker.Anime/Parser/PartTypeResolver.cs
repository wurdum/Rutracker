using System;
using System.Collections.Generic;
using System.Linq;
using Rutracker.Anime.Parser.Tokenizers;

namespace Rutracker.Anime.Parser
{
    public class PartTypeResolver
    {
        private readonly IEnumerable<PartTypePattern> _patterns;

        public PartTypeResolver(IEnumerable<PartTypePattern> patterns) {
            _patterns = patterns;
        }

        public TokenType getType(String part) {
            foreach (var pattern in _patterns.Where(pattern => pattern.isSatisfy(part)))
                return pattern.getPartType();

            return TokenType.Info;
        }

        public static PartTypeResolver Default {
            get {
                var partsRxs = new Dictionary<string, string> {
                    { "Traits", "(19|20)\\d{2}(\\-\\d{4})?((\\s+)?гг?(\\.|\\,))?(\\,?(\\s+)?[а-яА-Яa-zA-Z0-9]*)+" },
                    { "Series", "[\\d\\-\\+]+(\\s+)?(из|of)(\\s+)?([\\d\\-\\+\\?\\>]+|[xXхХ]+)" },
                    { "AvailTracks", "(([\\,\\+(\\s+)?]+)?(RUS(\\s+)?\\((int|ext)\\.?\\)|RUS|JAP|JAP(\\s+)?\\((int|ext)\\.?\\)|SUB|SUB(\\s+)?\\((int|ext)\\.?\\)|ENG|ENG(\\s+)?\\((int|ext)\\.?\\)))+" },
                    { "Type", "(([\\-\\+\\s+\\d]+)?(movie|specials?|ova|tv|oav|sp|ona)([\\-\\+\\s+\\d]+)?)+" }
                };

                return new PartTypeResolver(partsRxs.Select(p => new PartTypePattern(p.Value, (TokenType)Enum.Parse(typeof(TokenType), p.Key))));
            }
        }
    }
}