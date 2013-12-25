using System;
using System.Collections.Generic;

namespace Rutracker.Anime.Parser.Parts
{
    public class TracksParser {
        public static IEnumerable<string> getTracks(String value) {
            return value.Split(new[] { ",", "+", " " }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}