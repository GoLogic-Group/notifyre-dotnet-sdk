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
