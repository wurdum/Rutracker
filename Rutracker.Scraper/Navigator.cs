namespace Rutracker.Scraper
{
    public class Navigator {
        private readonly HttpProvider _httpProvider;
        private readonly UrlBuilder _urlBuilder;

        public Navigator(HttpProvider httpProvider, UrlBuilder urlBuilder) {
            _httpProvider = httpProvider;
            _urlBuilder = urlBuilder;
        }

        public string GetForumPage(int id) {
            var url = _urlBuilder.GetForumPageUrl(id);
            return _httpProvider.GetPage(url);
        }

        public string GetTopicPage(int id) {
            var url = _urlBuilder.GetTopicPageUrl(id);
            return _httpProvider.GetPage(url);
        }
    }
}