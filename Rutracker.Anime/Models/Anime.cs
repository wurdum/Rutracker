using System.Collections.Generic;
using System.Linq;

namespace Rutracker.Anime.Models
{
    public class Anime
    {
        public Anime() {
            OtherInfo = new List<string>();
        }

        public string Title { get; set; }
        public IEnumerable<string> Names { get; set; }
        public Series Series { get; set; }
        public Traits Traits { get; set; }
        public IEnumerable<Type> Types { get; set; }
        public IEnumerable<string> Tracks { get; set; }
        public IList<string> OtherInfo { get; set; }

        #region equality members

        protected bool Equals(Anime other) {
            return Names.SequenceEqual(other.Names) && Equals(Series, other.Series) && Equals(Traits, other.Traits) &&
                   Types.SequenceEqual(other.Types) && Tracks.SequenceEqual(other.Tracks);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return Equals((Anime) obj);
        }

        public override int GetHashCode() {
            unchecked {
                var hashCode = (Names != null ? Names.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Series != null ? Series.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Traits != null ? Traits.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Types != null ? Types.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Tracks != null ? Tracks.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion

        public override string ToString() {
            return string.Format("Names: {0}, Series: {1}, Traits: {2}, Types: {3}, Tracks: {4}, OtherInfo: {5}",
                "[" + string.Join(", ", Names) + "]", Series, Traits, "[" + string.Join(", ", Types) + "]",
                "[" + string.Join(", ", Tracks) + "]", "[" + string.Join(", ", OtherInfo) + "]");
        }

        public enum Type
        {
            TV = 1,
            Movie,
            OVA,
            ONA,
            Special
        }
    }
}