using System;
using System.Collections.Generic;
using Rutracker.Anime.Models;

namespace Rutracker.Anime.Parser.Parts
{
    public class TypesParser
    {
        public static List<AnimeType> GetType(String value) {
            var types = new List<AnimeType>();
            var valueLower = value.ToLower();

            foreach (var typeName in Enum.GetNames(typeof(AnimeType))) {
                if (valueLower.Contains(typeName.ToLower()))            
                    types.Add((AnimeType)Enum.Parse(typeof(AnimeType), typeName));
            }

            return types;
        }
    }
}