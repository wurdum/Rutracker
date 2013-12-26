using System;
using System.Collections.Generic;

namespace Rutracker.Anime.Parser.Parts
{
    public class TracksParser 
    {
        public IEnumerable<string> Parse(String part) {
            return part.Split(new[] { ",", "+", " " }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}