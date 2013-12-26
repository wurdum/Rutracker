using System;
using System.Linq;
using Rutracker.Anime.Models;
using Rutracker.Anime.Parser.Parts;

namespace Rutracker.Anime.Parser
{
    public class TitleParser
    {
        private readonly PartTypeResolver _typeResolver;
        private readonly SeriesParser _seriesParser = new SeriesParser();
        private readonly TracksParser _tracksParser = new TracksParser();
        private readonly TraitsParser _traitsParser = new TraitsParser();
        private readonly TypesParser _typesParser = new TypesParser();

        public TitleParser(PartTypeResolver typeResolver) {
            _typeResolver = typeResolver;
        }

        public AnimeTitle Parse(string title) {
            var animeTitle = new AnimeTitle();

            var end = ParseNames(title, animeTitle);
            ParseParts(title, animeTitle, end);

            return animeTitle;
        }

        private int ParseNames(string title, AnimeTitle animeTitle) {
            var startSearch = title.LastIndexOf('/');
            if (startSearch == -1)
                startSearch = 0;

            var bracketPos = IndexOfBracket(title, startSearch);
            var namesPart = title.Substring(0, bracketPos);
            var names = namesPart.Split(new[] {"/"}, StringSplitOptions.RemoveEmptyEntries).Select(s => {
                var bp = IndexOfBracket(s);
                return (bp == -1 ? s : s.Substring(0, bp)).Trim();
            }).ToArray();

            animeTitle.Names = names;
            return bracketPos;
        }

        private int IndexOfBracket(string str, int searchFrom = 0) {
            var squareBracketIndex = str.IndexOf('[', searchFrom);
            var roundBracketIndex = str.IndexOf('(', searchFrom);

            int end;
            if (squareBracketIndex == -1 || roundBracketIndex == -1)
                end = squareBracketIndex > roundBracketIndex ? squareBracketIndex : roundBracketIndex;
            else
                end = squareBracketIndex < roundBracketIndex ? squareBracketIndex : roundBracketIndex;

            return end;
        }

        private void ParseParts(string title, AnimeTitle animeTitle, int start) {
            var partStartIndex = start;
            var parenthesesHandler = new ParenthesesHandler(title);

            for (var i = partStartIndex; i < title.Length; i++) {
                parenthesesHandler.SetPosition(i);

                if (parenthesesHandler.IsAtStartBound() && !title.Substring(i).Contains("/")) {
                    partStartIndex = i;
                    continue;
                }

                if (parenthesesHandler.IsAtEndBound()) {
                    var value = title.Substring(partStartIndex, i + 1 - partStartIndex).Trim();
                    if (!ReadBlock(animeTitle, value))
                        continue;

                    partStartIndex = i + 1;
                }
            }
        }

        private bool ReadBlock(AnimeTitle animeTitle, string value) {
            value = RemoveParentheses(value);
            if (string.IsNullOrWhiteSpace(value))
                return false;

            var partType = _typeResolver.getType(value);

            switch (partType) {
                case PartTypePattern.PartType.AvailTracks:
                    animeTitle.Tracks = _tracksParser.Parse(value);
                    break;
                case PartTypePattern.PartType.Series:
                    animeTitle.Series = _seriesParser.Parse(value);
                    break;
                case PartTypePattern.PartType.Traits:
                    animeTitle.Traits = _traitsParser.Parse(value);
                    break;
                case PartTypePattern.PartType.Type:
                    animeTitle.Types = _typesParser.Parse(value);
                    break;
                default:
                    animeTitle.OtherInfo.Add(value);
                    break;
            }

            return true;
        }

        private string RemoveParentheses(string value) {
            var length = value.Length;
            if (length == 0 || !InParentheses(value))
                return value;

            int start = 0, end = length;
            var chars = new[] { '[', ']', '(', ')' };

            if (chars.Contains(value[start]))
                start++;
            if (chars.Contains(value[end - 1]))
                end--;
            if (start >= end)
                return "";

            return value.Substring(start, end - start);
        }

        private bool InParentheses(string value) {
            var length = value.Length;
            return new[] {'(', '['}.Contains(value[0]) && new[] {')', ']'}.Contains(value[length - 1]);
        }

        private class ParenthesesHandler
        {

            private readonly string _str;
            private int _pos;
            private int _parenthesesDepth;
            private bool _atStartBound;
            private bool _atEndBound;

            public ParenthesesHandler(String str) {
                _str = str;
            }

            public bool IsAtStartBound() {
                return _atStartBound;
            }

            public bool IsAtEndBound() {
                return _atEndBound;
            }

            public void SetPosition(int pos) {
                _pos = pos;

                HandlePositionChanging();
            }

            private void HandlePositionChanging() {
                var currentChar = _str[_pos];

                switch (currentChar) {
                    case '(':
                    case '[':
                        if (_parenthesesDepth == 0)
                            _atStartBound = true;

                        _parenthesesDepth++;
                        break;
                    case ')':
                    case ']':
                        if (_parenthesesDepth > 0)
                            _parenthesesDepth--;
                        if (_parenthesesDepth == 0)
                            _atEndBound = true;

                        break;
                    default:
                        _atStartBound = false;
                        _atEndBound = false;
                        break;
                }
            }
        }
    }
}