using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Notifyre.Constants;
using Notifyre.Infrastructure.Utils;
using Notifyre.Interfaces;
using Notifyre.Models;
using Notifyre.Services.Fax.FaxSend.SubmitFax.GetDocumentStatus;
using Notifyre.Services.Fax.FaxSend.SubmitFax.SendFax;
using Notifyre.Services.Fax.FaxSend.SubmitFax.SubmitDocument;

namespace Notifyre.Services.Fax.FaxSend
{
    public class FaxSendService : NotifyreClient
    {
        protected override string Path => "fax/send";
        protected string DownloadEndpoint => "download";
        protected string RecipientsEndpoint => "recipients";
        protected string ConversionEndpoint => "conversion";
        protected string CoverPagesEndpoint => "fax/coverpages";
        private TimeSpan _conversionTimeout => TimeSpan.FromSeconds(100);

        public FaxSendService(NotifyreConfiguration notifyreConfiguration) : base(notifyreConfiguration)
        {
        }

        public FaxSendService(IHttpHandler handler) : base(handler)
        {
        }

        public async Task<ListSentFaxesResponse> ListSentFaxesAsync(
            ListSentFaxesRequest request
        )
        {
            var uri = UrlUtil.CreateUrlWithQuery(request, Address);
            var result = await _HttpClient.GetAsync(uri).ConfigureAwait(false);
            return (await ReadJsonResponse<ListSentFaxesResponse>(result).ConfigureAwait(false)).Payload;
        }

        public async Task<DownloadFaxResponse> DownloadSentFaxAsync(
            DownloadFaxRequest request
        )
        {
            var uri = UrlUtil.CreateUrlWithQuery(request, Address, RecipientsEndpoint, request.RecipientID.ToString(), DownloadEndpoint);
            var result = await _HttpClient.GetAsync(uri).ConfigureAwait(false);
            return (await ReadJsonResponse<DownloadFaxResponse>(result).ConfigureAwait(false)).Payload;
        }

        public async Task<List<ListCoverPagesResponse>> ListCoverPagesAsync()
        {
            var uri = UrlUtil.CreateUrl(BasePath, CoverPagesEndpoint);
            var result = await _HttpClient.GetAsync(uri).ConfigureAwait(false);
            return (await ReadJsonResponse<List<ListCoverPagesResponse>>(result).ConfigureAwait(false)).Payload;
        }

        public async Task<SubmitFaxResponse> SubmitFaxAsync(
            SubmitFaxRequest request
        )
        {
            var uploads = await Task.WhenAll(request.Faxes.Documents.Select(x => UploadDocument(x)));
            if (!uploads.All(x => x.Success))
            {
                var distinctErrorCodes = uploads.Where(x => !x.Success).Select(x => x.StatusCode).Distinct();
                int statusCode = distinctErrorCodes.Count() == 1 ? distinctErrorCodes.FirstOrDefault() : (int)HttpStatusCode.BadRequest;
                throw new NotifyreException("The document upload process failed")
                {
                    Success = false,
                    StatusCode = statusCode,
                    Errors = uploads.Select(
                        (x, i) => x.Success ? null : $"File with client reference {request.Faxes.Documents[i].ClientReference} failed to upload with error message: {x.Message}")
                        .Where(x => !string.IsNullOrEmpty(x))
                        .ToList()
                };
            }
            var results = await Task.WhenAll(uploads.Select(x => ObserveDocumentStatus(x.Payload.FileName)));
            if (results.All(x => x.Success))
            {
                // Submit fax
                var sendFaxRequest = new SendFaxRequest(request, results.Select(x => x.Payload.ID).ToArray());
                var uri = UrlUtil.CreateUrl(Address);
                var body = JsonUtil.CreateBody(sendFaxRequest);
                var result = await _HttpClient.PostAsync(uri, body).ConfigureAwait(false);
                return (await ReadJsonResponse<SubmitFaxResponse>(result).ConfigureAwait(false)).Payload;
            }
            else
            {
                throw new NotifyreException("The document conversion process failed")
                {
                    Success = false,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Errors = results.Select(
                        (x, i) => x.Success ? null : $"File with client reference {request.Faxes.Documents[i].ClientReference} failed to convert with error message: {x.Message}")
                            .Where(x => !string.IsNullOrEmpty(x))
                            .ToList()
                };
            }
        }

        private async Task<BaseResponse<SubmitDocumentResponse>> UploadDocument(SubmittedDocument document)
        {
            try
            {
                var request = new SubmitDocumentRequest()
                {
                    ContentType = document.ContentType,
                    Base64Str = document.Base64Str
                };
                var result = await SubmitDocumentAsync(request);
                return new BaseResponse<SubmitDocumentResponse>(true, (int)HttpStatusCode.OK) { Payload = result };
            }
            catch (NotifyreException ex)
            {
                return new BaseResponse<SubmitDocumentResponse>(false, ex.StatusCode, ex.Message);
            }
        }

        private async Task<BaseResponse<GetDocumentStatusResponse>> ObserveDocumentStatus(string id)
        {
            DateTime conversionStart = DateTime.UtcNow;
            while (conversionStart.Add(_conversionTimeout).CompareTo(DateTime.UtcNow) > 0)
            {
                var request = new GetDocumentStatusRequest() { FileName = id };

                try
                {
                    var result = await GetDocumentStatusAsync(request);
                    if (result.Status == DocumentConversionStatusType.Successful)
                    {
                        return new BaseResponse<GetDocumentStatusResponse>(true, (int)HttpStatusCode.OK) { Payload = result };
                    }
                    else if (result.Status == DocumentConversionStatusType.Failed)
                    {
                        return new BaseResponse<GetDocumentStatusResponse>(false, (int)HttpStatusCode.InternalServerError, DocumentConversionStatusType.Failed);
                    }
                }
                catch (NotifyreException ex)
                {
                    if (ex.StatusCode != (int)HttpStatusCode.NotFound)
                    {
                        return new BaseResponse<GetDocumentStatusResponse>(false, ex.StatusCode, DocumentConversionStatusType.Failed);
                    }
                }
                await Task.Delay(3 * 1000);
            }
            return new BaseResponse<GetDocumentStatusResponse>(false, (int)HttpStatusCode.BadRequest, DocumentConversionStatusType.Failed);
        }

        private async Task<SubmitDocumentResponse> SubmitDocumentAsync(
            SubmitDocumentRequest request
        )
        {
            var uri = UrlUtil.CreateUrl(Address, ConversionEndpoint);
            var body = JsonUtil.CreateBody(request);
            var result = await _HttpClient.PostAsync(uri, body).ConfigureAwait(false);
            return (await ReadJsonResponse<SubmitDocumentResponse>(result).ConfigureAwait(false)).Payload;
        }

        private async Task<GetDocumentStatusResponse> GetDocumentStatusAsync(
            GetDocumentStatusRequest request
        )
        {
            var uri = UrlUtil.CreateUrl(Address, ConversionEndpoint, request.FileName);
            var result = await _HttpClient.GetAsync(uri).ConfigureAwait(false);
            return (await ReadJsonResponse<GetDocumentStatusResponse>(result).ConfigureAwait(false)).Payload;
        }
    }
}
