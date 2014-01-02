﻿using Rutracker.Anime.Parser.Tokenizers;

namespace Rutracker.Anime.Parser
{
    public class PartParsers {
        public SeriesTokenizer SeriesTokenizer { get; set; }
        public TracksTokenizer TracksTokenizer { get; set; }
        public TraitsTokenizer TraitsTokenizer { get; set; }
        public TypesTokenizer TypesTokenizer { get; set; }
    }
}