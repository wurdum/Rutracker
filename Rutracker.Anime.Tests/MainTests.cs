using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using Newtonsoft.Json;
using Rutracker.Anime.Parser;
using Rutracker.Anime.Parser.Parts;

namespace Rutracker.Anime.Tests
{
    [TestFixture(Category = "slow", Ignore = true)]
    public class MainTests
    {
        [Test]
        public void MainTest() {
            var titleParser = new TitleParser(PartTypeResolver.Default, new PartParsers {
                SeriesTokenizer = new SeriesTokenizer(),
                TracksTokenizer = new TracksTokenizer(),
                TraitsTokenizer = new TraitsTokenizer(),
                TypesParser = new TypesParser()
            });

            var list = new List<Models.Anime>();
            foreach (var title in Resources.GetRawTitles()) {
                Models.Anime anime = null;
                try {
                    anime = titleParser.Parse(title);
                } catch (Exception ex) {
                    Console.WriteLine(title);
                    Console.WriteLine(ex.StackTrace);
                }

                if (anime == null)
                    continue;

                list.Add(anime);
            }

            var json = JsonConvert.SerializeObject(list, Formatting.Indented);
            File.WriteAllText(Path.Combine(Resources.ResourcesPath, "result.json"), json);
        }
    }
}