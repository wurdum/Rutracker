using System;

namespace Rutracker.Scraper.Exceptions
{
    public class ParseHtmlException : Exception
    {
        public ParseHtmlException() {}
        public ParseHtmlException(string message) : base(message) {}
        public ParseHtmlException(string message, string html) : base(message) {
            Html = html;
        }

        public string Html { get; set; }
    }
}