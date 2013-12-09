using System;

namespace Rutracker.Scraper
{
    public class UrlBuilder
    {
        public virtual string GetLoginPageUrl() {
            return "http://login.rutracker.org/forum/login.php";
        }

        public virtual string GetForumPageUrl(int id) {
            return "http://rutracker.org/forum/viewforum.php?f=" + id;
        }

        public virtual string GetTopicPageUrl(int id) {
            return "http://rutracker.org/forum/viewtopic.php?t=" + id;
        }

        public virtual string GetForumPageUrl(int id, int start) {
            if (start < 0)
                throw new ArgumentOutOfRangeException("Url start position can't be negative");

            if (start == 0)
                return GetForumPageUrl(id);

            return string.Format("http://rutracker.org/forum/viewforum.php?f={0}&start={1}", id, start);
        }
    }
}