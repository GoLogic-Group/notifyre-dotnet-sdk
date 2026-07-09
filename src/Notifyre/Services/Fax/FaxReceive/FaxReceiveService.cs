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
        protected string DownloadAndDeleteEndpoint => "download-and-delete";
        protected string NumbersEndpoint => "fax/numbers";

        public FaxReceiveService(NotifyreConfiguration notifyreConfiguration) : base(notifyreConfiguration)
        {

        }

        public FaxReceiveService(IHttpHandler handler) : base(handler)
        {
        }

        public async Task<ListReceivedFaxesResponse> ListReceivedFaxesAsync(
            ListReceivedFaxesRequest request
        )
        {
            var uri = UrlUtil.CreateUrlWithQuery(request, Address);
            var result = await _HttpClient.GetAsync(uri).ConfigureAwait(false);
            return (await ReadJsonResponse<ListReceivedFaxesResponse>(result).ConfigureAwait(false)).Payload;
        }

        public async Task<DownloadReceivedFaxResponse> DownloadReceivedFaxAsync(
            DownloadReceivedFaxRequest request
        )
        {
            var uri = UrlUtil.CreateUrl(Address, request.FaxID.ToString(), DownloadEndpoint);
            var result = await _HttpClient.GetAsync(uri).ConfigureAwait(false);
            return (await ReadJsonResponse<DownloadReceivedFaxResponse>(result).ConfigureAwait(false)).Payload;
        }

        public async Task<DownloadAndDeleteReceivedFaxResponse> DownloadAndDeleteReceivedFaxAsync(
            DownloadAndDeleteReceivedFaxRequest request
        )
        {
            var uri = UrlUtil.CreateUrl(Address, request.FaxID.ToString(), DownloadAndDeleteEndpoint);
            var body = JsonUtil.CreateBody(request);
            var result = await _HttpClient.PostAsync(uri, body).ConfigureAwait(false);
            return (await ReadJsonResponse<DownloadAndDeleteReceivedFaxResponse>(result).ConfigureAwait(false)).Payload;
        }

        public async Task<ListFaxNumbersResponse> ListFaxNumbersAsync()
        {
            var uri = UrlUtil.CreateUrl(BasePath, NumbersEndpoint);
            var result = await _HttpClient.GetAsync(uri).ConfigureAwait(false);
            return (await ReadJsonResponse<ListFaxNumbersResponse>(result).ConfigureAwait(false)).Payload;
        }
    }
}
