using System;

namespace Rutracker.Scraper
{
    public class HttpProvider
    {
        public virtual HttpProvider Authorize(string login, string pass) {
            throw new NotImplementedException();
        }

        public virtual string GetPage(string url) {
            throw new NotImplementedException();
        }
    }
}