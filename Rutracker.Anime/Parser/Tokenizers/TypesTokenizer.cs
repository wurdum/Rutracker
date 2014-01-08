using System;
using System.Collections.Generic;

namespace Rutracker.Anime.Parser.Tokenizers
{
    public class TypesTokenizer : TokenizerBase
    {
        private static readonly string[] EnumNames = Enum.GetNames(typeof (Models.Anime.Type));

        public TypesTokenizer() {}

        public TypesTokenizer(string rx) : base(rx) {}

        public override TokenType TokenType {
            get { return TokenType.AnimeType; }
        }

        public override object Tokenize(string value) {
            var types = new List<Models.Anime.Type>();
            var valueLower = value.ToLower();

            foreach (var typeName in EnumNames) {
                if (valueLower.Contains(typeName.ToLower()))            
                    types.Add((Models.Anime.Type)Enum.Parse(typeof(Models.Anime.Type), typeName));
            }

            return types;
        }

        public override void UpdateModel(Models.Anime model, string lexeme) {
            model.Types = (IEnumerable<Models.Anime.Type>)Tokenize(lexeme);
        }
    }
}