using Notifyre.Services.Fax.FaxReceive;
using Notifyre.Services.Fax.FaxSend;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notifyre
{
    public class FaxService
    {
        private readonly FaxReceiveService _receiveService;
        private readonly FaxSendService _sendService;

        public FaxService(FaxReceiveService receiveService, FaxSendService sendService)
        {
            _receiveService = receiveService;
            _sendService = sendService;
        }

        public Task<ListReceivedFaxesResponse> ListReceivedFaxesAsync(ListReceivedFaxesRequest request) => _receiveService.ListReceivedFaxesAsync(request);

        public Task<DownloadReceivedFaxResponse> DownloadReceivedFaxAsync(DownloadReceivedFaxRequest request) => _receiveService.DownloadReceivedFaxAsync(request);

        public Task<ListFaxNumbersResponse> ListFaxNumbersAsync() => _receiveService.ListFaxNumbersAsync();

        public Task<ListSentFaxesResponse> ListSentFaxesAsync(ListSentFaxesRequest request) => _sendService.ListSentFaxesAsync(request);

        public Task<DownloadFaxResponse> DownloadSentFaxAsync(DownloadFaxRequest request) => _sendService.DownloadSentFaxAsync(request);

        public Task<List<ListCoverPagesResponse>> ListCoverPagesAsync() => _sendService.ListCoverPagesAsync();

        public Task<SubmitFaxResponse> SubmitFaxAsync(SubmitFaxRequest request) => _sendService.SubmitFaxAsync(request);

    }
}
