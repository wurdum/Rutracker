using Rutracker.Anime.Parser.Parts;

namespace Rutracker.Anime.Parser
{
    public class PartParsers {
        public SeriesParser SeriesParser { get; set; }
        public TracksParser TracksParser { get; set; }
        public TraitsParser TraitsParser { get; set; }
        public TypesParser TypesParser { get; set; }
    }
}