﻿using Rutracker.Anime.Parser.Parts;

namespace Rutracker.Anime.Parser
{
    public class PartParsers {
        public SeriesTokenizer SeriesTokenizer { get; set; }
        public TracksParser TracksParser { get; set; }
        public TraitsTokenizer TraitsTokenizer { get; set; }
        public TypesParser TypesParser { get; set; }
    }
}