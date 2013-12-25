namespace Rutracker.Anime.Models
{
    public class Series
    {
        private readonly int? _from;
        private readonly int? _number;
        private readonly int? _total;

        public Series(int? from, int? number, int? total) {
            _from = from;
            _number = number;
            _total = total;
        }

        public Series(int? number, int? total) : this(1, number, total) {}

        public int? GetFrom() {
            return _from;
        }

        public int? GetNumber() {
            return _number;
        }

        public int? GetTotal() {
            return _total;
        }
    }
}