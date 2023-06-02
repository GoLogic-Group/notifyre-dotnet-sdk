using System.Threading.Tasks;
using Notifyre.Infrastructure.Utils;
using Notifyre.Interfaces;

namespace Notifyre
{
    public class SmsService : NotifyreClient
    {
        protected override string Path => "sms/send";
        protected string RecipientsEndpoint => "recipients";
        protected string RepliesEndpoint => "sms/received";
        protected string NumbersEndpoint => "sms/numbers";
        protected string PricesEndpoint => "sms/send/prices";

        public SmsService(NotifyreConfiguration notifyreConfiguration) : base(notifyreConfiguration)
        {
        }

        public SmsService(IHttpHandler handler) : base(handler)
        {
        }

        public async Task<SubmitSmsResponse> SubmitSmsAsync(
            SubmitSmsRequest request
        )
        {
            var uri = UrlUtil.CreateUrl(Address);
            var body = JsonUtil.CreateBody(request);
            var result = await _HttpClient.PostAsync(uri, body).ConfigureAwait(false);
            return (await ReadJsonResponse<SubmitSmsResponse>(result).ConfigureAwait(false)).Payload;
        }

        public async Task<ListSentSmsResponse> ListSentSmsAsync(
            ListSentSmsRequest request
        )
        {
            var uri = UrlUtil.CreateUrlWithQuery(request, Address);
            var result = await _HttpClient.GetAsync(uri).ConfigureAwait(false);
            return (await ReadJsonResponse<ListSentSmsResponse>(result).ConfigureAwait(false)).Payload;
        }

        public async Task<GetSentSmsResponse> GetSentSmsAsync(
            GetSentSmsRequest request
        )
        {
            var uri = UrlUtil.CreateUrl(Address, request.MessageID.ToString());
            var result = await _HttpClient.GetAsync(uri).ConfigureAwait(false);
            return (await ReadJsonResponse<GetSentSmsResponse>(result).ConfigureAwait(false)).Payload;
        }
        public async Task<GetSentSmsRecipientResponse> GetSentSmsRecipientAsync(
            GetSentSmsRecipientRequest request
        )
        {
            var uri = UrlUtil.CreateUrl(Address, request.MessageID.ToString(), RecipientsEndpoint, request.RecipientID.ToString());
            var result = await _HttpClient.GetAsync(uri).ConfigureAwait(false);
            return (await ReadJsonResponse<GetSentSmsRecipientResponse>(result).ConfigureAwait(false)).Payload;
        }
        public async Task<ListSmsRepliesResponse> ListSmsRepliesAsync(
            ListSmsRepliesRequest request
        )
        {
            var uri = UrlUtil.CreateUrlWithQuery(request, BasePath, RepliesEndpoint);
            var result = await _HttpClient.GetAsync(uri).ConfigureAwait(false);
            return (await ReadJsonResponse<ListSmsRepliesResponse>(result).ConfigureAwait(false)).Payload;
        }

        public async Task<GetSmsReplyResponse> GetSmsReplyAsync(
            GetSmsReplyRequest request
        )
        {
            var uri = UrlUtil.CreateUrl(BasePath, RepliesEndpoint, request.ReplyID);
            var result = await _HttpClient.GetAsync(uri).ConfigureAwait(false);
            return (await ReadJsonResponse<GetSmsReplyResponse>(result).ConfigureAwait(false)).Payload;
        }

        public async Task<ListSmsNumbersResponse> ListSmsNumbersAsync()
        {
            var uri = UrlUtil.CreateUrl(BasePath, NumbersEndpoint);
            var result = await _HttpClient.GetAsync(uri).ConfigureAwait(false);
            return (await ReadJsonResponse<ListSmsNumbersResponse>(result).ConfigureAwait(false)).Payload;
        }

        public async Task<ListFaxPricesResponse> ListPricesAsync()
        {
            var uri = UrlUtil.CreateUrl(BasePath, PricesEndpoint);
            var result = await _HttpClient.GetAsync(uri).ConfigureAwait(false);
            return (await ReadJsonResponse<ListFaxPricesResponse>(result).ConfigureAwait(false)).Payload;
        }
    }
}
