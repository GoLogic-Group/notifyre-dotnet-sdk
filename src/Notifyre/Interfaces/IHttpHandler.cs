using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Notifyre.Interfaces
{
    public interface IHttpHandler
    {
        Task<HttpResponseMessage> GetAsync(Uri url);
        Task<HttpResponseMessage> PostAsync(Uri url, HttpContent content);
        Task<HttpResponseMessage> PostCompressedAsync(Uri url, HttpContent content);
        Task<HttpResponseMessage> DeleteAsync(Uri url, HttpContent content);
        Task<HttpResponseMessage> UpdateAsync(Uri url, HttpContent content);
    }
}
