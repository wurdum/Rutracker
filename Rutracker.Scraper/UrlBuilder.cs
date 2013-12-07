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
    }
}