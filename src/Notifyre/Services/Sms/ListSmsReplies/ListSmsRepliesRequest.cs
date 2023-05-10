using Notifyre.Infrastructure.Annotations;
using System;

namespace Notifyre
{
    public class ListSmsRepliesRequest
    {
        [QueryParam]
        public long? FromDate { get; set; }

        [QueryParam]
        public long? ToDate { get; set; }

        [QueryParam]
        public ListSmsRepliesRequestSortTypes? Sort { get; set; }

        [QueryParam]
        public string ToNumber { get; set; }

        [QueryParam]
        public string FromNumber { get; set; }

        [QueryParam]
        public Guid RecipientID { get; set; }

        [QueryParam]
        public bool IncludeReplyContent { get; set; } = false;

        [QueryParam]
        public int Limit { get; set; } = 100;

        [QueryParam]
        public int Skip { get; set; } = 0;
    }

    public enum ListSmsRepliesRequestSortTypes
    {
        Asc,
        Desc
    }
}
