using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using HtmlAgilityPack;
using Rutracker.Scraper.Exceptions;

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
                var tds = tr.Elements("td").ToArray();
                if (tds.Length != 5)
                    throw new ParseHtmlException("TR element contain less than 5 TD", tr.OuterHtml);

                var torrentSize = WebUtility.HtmlDecode(tds[2].InnerText).Trim();
                if (string.IsNullOrWhiteSpace(torrentSize))
                    continue;

                var a = tds[1].Element("a");
                var torrentName = CleanName(WebUtility.HtmlDecode(a.InnerText));
                var torrentUrl = ToFullUrl(a.Attributes["href"].Value);

                titles.Add(new TopicTitle {
                    Name = torrentName,
                    Url = torrentUrl,
                    Size = torrentSize
                });
            }

            return titles;
        }

        private string CleanName(string name) {
            return name.Trim().Replace("<wbr></wbr>", "");
        }

        private string ToFullUrl(string partialUrl) {
            if (string.IsNullOrWhiteSpace(partialUrl))
                throw new ArgumentNullException("partialUrl");
            
            if (partialUrl.StartsWith("http"))
                return partialUrl;

            var fullUrl = "http://rutracker.org/forum" + partialUrl.Substring(1);
            return fullUrl;
        }
    }

    public class TopicTitle {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Size { get; set; }

        #region equality members

        protected bool Equals(TopicTitle other) {
            return string.Equals(Name, other.Name) && string.Equals(Url, other.Url) && string.Equals(Size, other.Size);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return Equals((TopicTitle) obj);
        }

        public override int GetHashCode() {
            unchecked {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Url != null ? Url.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Size != null ? Size.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion
    }
}