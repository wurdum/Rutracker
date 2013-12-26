using System;
using System.Collections.Generic;
using AnimeType = Rutracker.Anime.Models.Anime.Type;

namespace Rutracker.Anime.Parser.Parts
{
    public class TypesParser
    {
        private static readonly string[] EnumNames = Enum.GetNames(typeof (AnimeType));

        public virtual IEnumerable<AnimeType> Parse(string value) {
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