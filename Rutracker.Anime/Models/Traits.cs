using System;
using System.Collections.Generic;
using System.Linq;

namespace Rutracker.Anime.Models
{
    public class Traits 
    {
        public Traits(int? year, IEnumerable<String> genres, String format) {
            Year = year;
            Genres = genres;
            Format = format;
        }

        public int? Year { get; private set; }
        public IEnumerable<string> Genres { get; private set; }
        public string Format { get; private set; }

        #region equality members

        protected bool Equals(Traits other) {
            return Year == other.Year && Genres.SequenceEqual(other.Genres) && string.Equals(Format, other.Format);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return Equals((Traits) obj);
        }

        public override int GetHashCode() {
            unchecked {
                var hashCode = Year.GetHashCode();
                hashCode = (hashCode*397) ^ (Genres != null ? Genres.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Format != null ? Format.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion

        public override string ToString() {
            return string.Format("Year: {0}, Genres: [{1}], Format: {2}", Year, string.Join(", ", Genres), Format);
        }
    }
}