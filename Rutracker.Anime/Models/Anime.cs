using System.Collections.Generic;

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