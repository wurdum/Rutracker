using System.Collections.Generic;

namespace Rutracker.Anime
{
    public static class Extensions
    {
        public static string SequenceToString<T>(this IEnumerable<T> self) {
            return "[" + string.Join(", ", self) + "]";
        }
    }
}