using System.Collections.Generic;
using System.Linq;

namespace Rutracker.Anime
{
    public static class Extensions
    {
        public static string SequenceToString<T>(this IEnumerable<T> self) {
            if (self == null || !self.Any())
                return string.Empty;

            return "[" + string.Join(", ", self) + "]";
        }
    }
}