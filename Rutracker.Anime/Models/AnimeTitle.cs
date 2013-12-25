using System.Collections.Generic;

namespace Rutracker.Anime.Models
{
    public class AnimeTitle
    {
        public AnimeTitle() {
            OtherInfo = new List<string>();
        }

        public IEnumerable<string> Names { get; set; }
        public Series Series { get; set; }
        public Traits Traits { get; set; }
        public IEnumerable<AnimeType> Types { get; set; }
        public IEnumerable<string> Tracks { get; set; }
        public IList<string> OtherInfo { get; set; }
    }
}