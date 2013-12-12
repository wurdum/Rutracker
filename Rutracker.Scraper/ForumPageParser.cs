using System;
using System.Collections.Generic;
using HtmlAgilityPack;

namespace Rutracker.Scraper
{
    public class ForumPageParser
    {
        public ForumPageParser(string page) {
            Page = page;
        }

        public string Page { get; private set; }

        public IEnumerable<TopicTitle> GetTitles() {
            var titles = new List<TopicTitle>(50);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(Page);

            var root = htmlDocument.DocumentNode;
            var trs = root.SelectNodes("/html/body/div[@id='body_container']/div[@id='page_container']/" +
                "div[@id='page_content']/table/tr/td[@id='main_content']/div[@id='main_content_wrap']/" +
                "table[@class='forumline forum']/tr[starts-with(@id, 'tr-')]");

            foreach (var tr in trs) {
                //
            }

            return titles;
        }
    }

    public class TopicTitle {}
}