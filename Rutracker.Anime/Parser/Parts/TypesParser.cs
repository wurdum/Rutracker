using System;
using System.Collections.Generic;

namespace Rutracker.Anime.Parser.Parts
{
    public class TypesParser
    {
        private static readonly string[] EnumNames = Enum.GetNames(typeof (Models.Anime.Type));

        public IEnumerable<Models.Anime.Type> Parse(string value) {
            var types = new List<Models.Anime.Type>();
            var valueLower = value.ToLower();

            foreach (var typeName in EnumNames) {
                if (valueLower.Contains(typeName.ToLower()))            
                    types.Add((Models.Anime.Type)Enum.Parse(typeof(Models.Anime.Type), typeName));
            }

            return types;
        }
    }
}