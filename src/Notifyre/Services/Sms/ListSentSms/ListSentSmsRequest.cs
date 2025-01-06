using Notifyre.Infrastructure.Annotations;
using System;

namespace Notifyre
{
    public class ListSentSmsRequest
    {
        [QueryParam]
        public ListSmsRequestStatusTypes? StatusType { get; set; }
        [QueryParam]
        public long? FromDate { get; set; }
        [QueryParam]
        public long? ToDate { get; set; }
        [QueryParam]
        public string Search { get; set; }
        [QueryParam]
        public ListSmsRequestSortTypes? Sort { get; set; }
        [QueryParam]
        public int Limit { get; set; } = 100;
        [QueryParam]
        public int Skip { get; set; } = 0;
        [QueryParam]
        public string ToNumber { get; set; }
        [QueryParam]
        public string FromNumber { get; set; }

    }

    public enum ListSmsRequestStatusTypes
    {
        Submitted,
        Processing,
        Sent,
        Failed
    }

    public enum ListSmsRequestSortTypes
    {
        Asc,
        Desc
    }
}
