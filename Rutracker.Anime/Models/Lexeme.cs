using Rutracker.Anime.Parser.Tokenizers;

namespace Rutracker.Anime.Models
{
    public struct Lexeme
    {
        public readonly TokenType Type;
        public readonly string String;

        public Lexeme(TokenType type, string s) {
            Type = type;
            String = s;
        }

        public bool Equals(Lexeme other) {
            return Type == other.Type && string.Equals(String, other.String);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj))
                return false;
            return obj is Lexeme && Equals((Lexeme) obj);
        }

        public override int GetHashCode() {
            unchecked {
                return ((int) Type*397) ^ (String != null ? String.GetHashCode() : 0);
            }
        }

        public override string ToString() {
            return string.Format("Type: {0}, S: {1}", Type, String);
        }
    }
}