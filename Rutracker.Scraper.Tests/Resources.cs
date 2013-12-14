using System;
using System.IO;
using System.Reflection;

namespace Rutracker.Scraper.Tests
{
    public static class Resources
    {
        public static string AppRoot = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
        public static string ResourcesPath = Path.Combine(AppRoot, "Resources");
        public static string ResponsesPath = Path.Combine(ResourcesPath, "Responses");
        public static string JsonsPath = Path.Combine(ResourcesPath, "Jsons");

        public static string GetJsonPath(string fileName) {
            return Path.Combine(JsonsPath, fileName);
        }

        public static string GetJsonText(string fileName) {
            var filePath = GetJsonPath(fileName);
            return File.ReadAllText(filePath);
        }

        private static string GetResponsePath(string fileName) {
            return Path.Combine(ResponsesPath, fileName);
        }

        public static string GetResponseText(string fileName) {
            var filePath = GetResponsePath(fileName);
            return File.ReadAllText(filePath);
        }
    }
}