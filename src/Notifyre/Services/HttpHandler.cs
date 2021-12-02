using Notifyre.Interfaces;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System;

namespace Notifyre.Services
{
    public class HttpHandler : IHttpHandler
    {
        private HttpClient _HttpClient = new HttpClient();

        internal HttpHandler(NotifyreConfiguration configuration)
        {
            var handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            _HttpClient = new HttpClient(handler);
            AddDefaultHeaders(configuration);
        }

        public async Task<HttpResponseMessage> GetAsync(Uri url)
        {
            return await _HttpClient.GetAsync(url);
        }

        public async Task<HttpResponseMessage> PostAsync(Uri url, HttpContent content)
        {
            return await _HttpClient.PostAsync(url, content);
        }

        public async Task<HttpResponseMessage> PostCompressedAsync(Uri url, HttpContent content)
        {
            var message = new HttpRequestMessage(HttpMethod.Post, url);
            message.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            message.Content = content;
            return await _HttpClient.SendAsync(message);
        }

        public async Task<HttpResponseMessage> DeleteAsync(Uri url, HttpContent content)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Content = content,
                Method = HttpMethod.Delete,
                RequestUri = url
            };
            return await _HttpClient.SendAsync(request);
        }

        private void AddDefaultHeaders(NotifyreConfiguration configuration)
        {
            _HttpClient.DefaultRequestHeaders.Add("x-api-token", configuration.ApiToken);
            _HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            _HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
            _HttpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            _HttpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
            _HttpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("br"));
            _HttpClient.DefaultRequestHeaders.Connection.Add("keep-alive");
            _HttpClient.DefaultRequestHeaders.Add("user-agent", configuration.NotifyreNetVersion);

        }

        public async Task<HttpResponseMessage> UpdateAsync(Uri url, HttpContent content)
        {
            return await _HttpClient.PutAsync(url, content);
        }
    }
}
