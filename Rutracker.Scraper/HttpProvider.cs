using System;
using System.IO;
using System.Net;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace Rutracker.Scraper
{
    public class HttpProvider
    {
        public HttpProvider() : this(Encoding.GetEncoding("windows-1251")) { }

        public HttpProvider(Encoding encoding) {
            Encoding = encoding;
            Cookies = new CookieContainer();
        }

        public bool IsAuthorized { get; private set; }
        public Encoding Encoding { get; private set; }
        public CookieContainer Cookies { get; private set; }

        public virtual async Task<HttpProvider> AuthorizeAsync(string loginUrl, string login, string pass, bool throwOnFail = false) {
            var credentials = string.Format("login_username={0}&login_password={1}&login=%C2%F5%EE%E4", login, pass);
            var postBody = Encoding.UTF8.GetBytes(credentials);
            
            var webRequest = CreateRequest(loginUrl, true);
            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.ContentLength = postBody.Length;
            using (var writer = webRequest.GetRequestStream())
                await writer.WriteAsync(postBody, 0, postBody.Length);

            var webResponse = (HttpWebResponse)await webRequest.GetResponseAsync();
            if (webResponse.StatusCode == HttpStatusCode.OK) {
                var content = await GetResponseBodyAsync(webResponse);
                Console.WriteLine(content);
            }

            IsAuthorized = !webRequest.RequestUri.Equals(webResponse.ResponseUri);
            if (!IsAuthorized && throwOnFail)
                throw new AuthenticationException("Rutracker rejected authorization request");

            return this;
        }

        public virtual async Task<string> GetPageAsync(string url) {
            var request = CreateRequest(url);
            var response = (HttpWebResponse)await request.GetResponseAsync();
            if (response.StatusCode != HttpStatusCode.OK)
                throw new WebException("Request to '" + request.RequestUri + "' failed, statur: " + response.StatusCode);

            var content = await GetResponseBodyAsync(response);
            return content;
        }

        private async Task<string> GetResponseBodyAsync(HttpWebResponse webResponse) {
            var stream = webResponse.GetResponseStream();
            if (stream == null)
                throw new NullReferenceException("Response stream from '" + webResponse.ResponseUri + "' is null");

            string content;
            using (var reader = new StreamReader(stream, Encoding))
                content = await reader.ReadToEndAsync();

            return content;
        }

        private HttpWebRequest CreateRequest(string url, bool isLogin = false) {
            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            webRequest.Headers[HttpRequestHeader.AcceptEncoding] = "gzip,deflate,sdch";
            webRequest.Headers[HttpRequestHeader.AcceptLanguage] = "ru-RU,ru;q=0.8,en-US;q=0.6,en;q=0.4,uk;q=0.2";
            webRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36";
            webRequest.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            webRequest.Headers[HttpRequestHeader.CacheControl] = "max-age=0";
            webRequest.KeepAlive = true;
            webRequest.Host = isLogin ? "login.rutracker.org" : "rutracker.org";
            webRequest.CookieContainer = Cookies;
            webRequest.AllowAutoRedirect = isLogin;
            return webRequest;
        }
    }
}