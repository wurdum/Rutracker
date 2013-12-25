using System;
using System.Text.RegularExpressions;

namespace Rutracker.Anime.Parser
{
    public class PartTypePattern
    {
        private readonly Regex _regex;
        private readonly PartType _partType;

        public PartTypePattern(String regex, PartType partType) {
            _partType = partType;
            _regex = new Regex(regex, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        public PartType getPartType() {
            return _partType;
        }

        public bool isSatisfy(String value) {
            return _regex.IsMatch(value);
        }

        public enum PartType
        {
            Traits,
            Series,
            AvailTracks,
            Type,
            Info
        }
    }
}