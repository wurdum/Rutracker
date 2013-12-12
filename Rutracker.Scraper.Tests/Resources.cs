﻿using System;
using System.IO;
using System.Reflection;

namespace Rutracker.Scraper.Tests
{
    public static class Resources
    {
        public static string AppRoot = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
        public static string ResourcesPath = Path.Combine(AppRoot, "Resources");
        public static string ResponsesPath = Path.Combine(ResourcesPath, "Responses");

        public static string GetResponseText(string fileName) {
            var filePath = Path.Combine(ResponsesPath, fileName);
            return File.ReadAllText(filePath);
        }
    }
}