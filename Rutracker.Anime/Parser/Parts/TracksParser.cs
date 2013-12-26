using System;
using System.Collections.Generic;

namespace Rutracker.Anime.Parser.Parts
{
    public class TracksParser 
    {
        public virtual IEnumerable<string> Parse(String part) {
            return part.Split(new[] { ",", "+", " " }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}