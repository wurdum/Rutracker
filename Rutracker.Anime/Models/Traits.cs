using System;
using System.Collections.Generic;

namespace Rutracker.Anime.Models
{
    public class Traits {

        private readonly int? _year;
        private readonly List<string> _genres;
        private readonly String _format;

        public Traits(int? year, List<String> genres, String format) {
            _year = year;
            _genres = genres;
            _format = format;
        }

        public int? GetYear() {
            return _year;
        }

        public List<String> GetGenres() {
            return _genres;
        }

        public String GetFormat() {
            return _format;
        }
    }
}