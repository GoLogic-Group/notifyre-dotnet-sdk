using Notifyre.Infrastructure.Annotations;
using System;

namespace Notifyre
{
    public class ListSmsRepliesRequest
    {
        [QueryParam]
        public DateTime? FromDate { get; set; }

        [QueryParam]
        public DateTime? ToDate { get; set; }

        [QueryParam]
        public ListSmsRepliesRequestSortTypes? Sort { get; set; }

        [QueryParam]
        public int Limit { get; set; } = 20;
    }

    public enum ListSmsRepliesRequestSortTypes
    {
        Asc,
        Desc
    }
}
