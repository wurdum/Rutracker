namespace Rutracker.Anime.Models
{
    public class Series
    {
        public Series(int? from, int? number, int? total) {
            From = from;
            Number = number;
            Total = total;
        }

        public Series(int? number, int? total) : this(1, number, total) {}

        public int? From { get; private set; }
        public int? Number { get; private set; }
        public int? Total { get; private set; }

        #region equality members

        protected bool Equals(Series other) {
            return From == other.From && Number == other.Number && Total == other.Total;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return Equals((Series) obj);
        }

        public override int GetHashCode() {
            unchecked {
                var hashCode = From.GetHashCode();
                hashCode = (hashCode*397) ^ Number.GetHashCode();
                hashCode = (hashCode*397) ^ Total.GetHashCode();
                return hashCode;
            }
        }

        #endregion
    }
}