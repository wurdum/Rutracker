using System;
using System.IO;
using System.Reflection;

namespace Rutracker.Anime.Tests
{
    public static class Resources
    {
        public static string AppRoot = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
        public static string ResourcesPath = Path.Combine(AppRoot, "Resources");
        public static string RawTitlesPath = Path.Combine(ResourcesPath, "titles.txt");

        public static string[] GetRawTitles() {
            return File.ReadAllLines(RawTitlesPath);
        }
    }
}