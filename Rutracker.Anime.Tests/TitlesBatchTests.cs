using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using NUnit.Framework;
using Newtonsoft.Json;
using Rutracker.Anime.Exceptions;
using Rutracker.Anime.Parser;
using Rutracker.Anime.Parser.Tokenizers;

namespace Rutracker.Anime.Tests
{
    [TestFixture(Category = "Slow", Ignore = true)]
    public class TitlesBatchTests
    {
        public object _consoleLock = new object();

        [Test]
        public void CompareResultingJsons() {
            var titlesList = Resources.GetRawTitles();
            var jsonModelsFromLexer = ParseTitles(titlesList);
            var jsonModelsFromFile = Resources.GetRawJson();

            CollectionAssert.AreEquivalent(jsonModelsFromFile, jsonModelsFromLexer);
        }

        //[Test]
        public void RewriteJson() {
            var titlesList = Resources.GetRawTitles();
            var jsonModelsFromParser = ParseTitles(titlesList);

            File.WriteAllLines(Path.Combine(Resources.ResourcesPath, "titles.json"), jsonModelsFromParser);
        }

        private IEnumerable<string> ParseTitles(IEnumerable<string> titlesList) {
            var inputTitles = new BlockingCollection<string>();
            var animeModels = new BlockingCollection<Models.Anime>();
            var jsonModels = new BlockingCollection<string>();

            var readLinesProcessor = Task.Factory.StartNew(() => {
                try {
                    foreach (var title in titlesList)
                        inputTitles.Add(title);
                } finally {
                    inputTitles.CompleteAdding();
                }
            });

            var titleProcessor = Task.Factory.StartNew(() => {
                try {
                    for (var i = 0; i < Environment.ProcessorCount; i++) {
                        Task.Factory.StartNew(() => {
                            var tokenizers = new List<TokenizerBase> {
                                new NamesTokenizer(),
                                new SeriesTokenizer(),
                                new TraitsTokenizer(),
                                new AudioAndSubsTokenizer(),
                                new TypesTokenizer(),
                                new InfoTokenizer()
                            };

                            var scanner = new Scanner(tokenizers);
                            var lexer = new Lexer(scanner, tokenizers);

                            var stopwatch = new Stopwatch();
                            foreach (var title in inputTitles.GetConsumingEnumerable()) {
                                try {
                                    stopwatch.Start();
                                    var titleUnc = title;
                                    Task.Factory.StartNew(() => animeModels.Add(lexer.Parse(titleUnc))).Wait(TimeSpan.FromSeconds(1));

                                    lock (_consoleLock) {
                                        stopwatch.Stop();
                                        Console.WriteLine("== " + title);
                                        Console.WriteLine("== " + stopwatch.ElapsedMilliseconds);
                                        stopwatch.Reset();
                                    }
                                } catch (ScannerException ex) {
                                    lock (_consoleLock) {
                                        Console.WriteLine("--- " + title);
                                        Console.WriteLine(ex.Message);
                                        Console.WriteLine(ex.StackTrace);
                                    }
                                } catch (TokenizerException ex) {
                                    lock (_consoleLock) {
                                        Console.WriteLine("--- " + title);
                                        Console.WriteLine(ex.Message);
                                        Console.WriteLine(ex.Lexeme);
                                        Console.WriteLine(ex.StackTrace);
                                    }
                                } catch (ArgumentOutOfRangeException ex) {
                                    lock (_consoleLock) {
                                        stopwatch.Stop();
                                        Console.WriteLine("== " + title);
                                        Console.WriteLine("== timeout");
                                        stopwatch.Reset();
                                    }
                                } catch (Exception ex) {
                                    lock (_consoleLock) {
                                        if (ex.InnerException != null)
                                            ex = ex.InnerException;

                                        Console.WriteLine("--- " + title);
                                        Console.WriteLine(ex.Message);
                                        Console.WriteLine(ex.StackTrace);
                                    }
                                }
                            }
                        }, TaskCreationOptions.AttachedToParent);
                    }
                } catch {}
            }).ContinueWith(t => animeModels.CompleteAdding());

            var serializeProcessor = Task.Factory.StartNew(() => {
                try {
                    foreach (var model in animeModels.GetConsumingEnumerable()) {
                        jsonModels.Add(JsonConvert.SerializeObject(model));
                    }
                } finally {
                    jsonModels.CompleteAdding();
                }
            });

            Task.WaitAll(readLinesProcessor, titleProcessor, serializeProcessor);
            return jsonModels;
        }
    }
}