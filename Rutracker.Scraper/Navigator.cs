using System.Threading.Tasks;

namespace Rutracker.Scraper
{
    public class Navigator {
        private readonly HttpProvider _httpProvider;
        private readonly UrlBuilder _urlBuilder;

        public Navigator(HttpProvider httpProvider, UrlBuilder urlBuilder) {
            _httpProvider = httpProvider;
            _urlBuilder = urlBuilder;
        }

        public async Task<string> GetForumPageAsync(int id) {
            var url = _urlBuilder.GetForumPageUrl(id);
            var page = await _httpProvider.GetPageAsync(url);
            return page;
        }

        public async Task<string> GetTopicPageAsync(int id) {
            var url = _urlBuilder.GetTopicPageUrl(id);
            var paage = await _httpProvider.GetPageAsync(url);
            return paage;
        }
    }
}