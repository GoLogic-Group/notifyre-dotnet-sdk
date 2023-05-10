using Notifyre.Infrastructure.Annotations;
using Notifyre.Infrastructure.Utils;
using System;

namespace Notifyre
{
    public class ListSentFaxesRequest
    {
        [QueryParam]
        public ListSentFaxesRequestStatusTypes? StatusType { get; set; }
        [QueryParam]
        public long FromDate { get; set; } = DateTime.UtcNow.AddDays(-30).ToUnixTimeStamp();
        [QueryParam]
        public long ToDate { get; set; } = DateTime.UtcNow.ToUnixTimeStamp();
        [QueryParam]
        public string Search { get; set; }
        [QueryParam]
        public ListSentFaxesRequestSortTypes? Sort { get; set; }
        [QueryParam]
        public int Limit { get; set; } = 20;
        [QueryParam]
        public int Skip { get; set; } = 0;

    }

    public enum ListSentFaxesRequestSortTypes
    {
        Asc,
        Desc
    }

    public enum ListSentFaxesRequestStatusTypes
    {
        Accepted,
        Successful,
        Queued,
        Failed,
        In_Progress
    }
}
