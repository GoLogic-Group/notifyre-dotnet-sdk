using System.Collections.Generic;
using System.Threading.Tasks;
using Notifyre.Infrastructure.Utils;
using Notifyre.Interfaces;

namespace Notifyre.Services.Fax.FaxReceive
{
    public class FaxReceiveService : NotifyreClient
    {
        protected override string Path => "fax/received";
        protected string DownloadEndpoint => "download";
        protected string NumbersEndpoint => "fax/numbers";

        public FaxReceiveService(NotifyreConfiguration notifyreConfiguration) : base(notifyreConfiguration)
        {

        }

        public FaxReceiveService(IHttpHandler handler) : base(handler)
        {
        }

        public async Task<List<ListReceivedFaxesResponse>> ListReceivedFaxesAsync(
            ListReceivedFaxesRequest request
        )
        {
            var uri = UrlUtil.CreateUrlWithQuery(request, Address);
            var result = await _HttpClient.GetAsync(uri).ConfigureAwait(false);
            return (await ReadJsonResponse<List<ListReceivedFaxesResponse>>(result).ConfigureAwait(false)).Payload;
        }

        public async Task<DownloadReceivedFaxResponse> DownloadReceivedFaxAsync(
            DownloadReceivedFaxRequest request
        )
        {
            var uri = UrlUtil.CreateUrl(Address, request.FaxID.ToString(), DownloadEndpoint);
            var result = await _HttpClient.GetAsync(uri).ConfigureAwait(false);
            return (await ReadJsonResponse<DownloadReceivedFaxResponse>(result).ConfigureAwait(false)).Payload;
        }

        public async Task<ListFaxNumbersResponse> ListFaxNumbersAsync()
        {
            var uri = UrlUtil.CreateUrl(BasePath, NumbersEndpoint);
            var result = await _HttpClient.GetAsync(uri).ConfigureAwait(false);
            return (await ReadJsonResponse<ListFaxNumbersResponse>(result).ConfigureAwait(false)).Payload;
        }
    }
}
