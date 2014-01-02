using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text.RegularExpressions;
using AnimeType = Rutracker.Anime.Models.Anime.Type;

namespace Rutracker.Anime.Parser.Parts
{
    public class TypesTokenizer : TokenizerBase
    {
        private static readonly string[] EnumNames = Enum.GetNames(typeof (AnimeType));

        public TypesTokenizer() {
            TokenRx = new Regex(ConfigurationManager.AppSettings[TokenType.ToString()], RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        public TypesTokenizer(string rxString) {
            TokenRx = new Regex(rxString, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        public override PartTypePattern.PartType TokenType {
            get { return PartTypePattern.PartType.Type; }
        }

        public override object Tokenize(string value) {
            var types = new List<AnimeType>();
            var valueLower = value.ToLower();

            foreach (var typeName in EnumNames) {
                if (valueLower.Contains(typeName.ToLower()))            
                    types.Add((AnimeType)Enum.Parse(typeof(AnimeType), typeName));
            }

            return types;
        }
    }
}