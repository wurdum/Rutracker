using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rutracker.Scraper
{
    public class Navigator
    {
        public const int TopicsPerPage = 50;

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

        public async Task<IEnumerable<string>> GetSeveralForumPagesAsync(int id, int[] pagesNumbers) {
            var pages = new List<string>(pagesNumbers.Length);
            foreach (var pagesNumber in pagesNumbers) {
                var url = _urlBuilder.GetForumPageUrl(id, TopicsPerPage*pagesNumber);
                var page = await _httpProvider.GetPageAsync(url);

                pages.Add(page);
            }

            return pages;
        }
    }
}