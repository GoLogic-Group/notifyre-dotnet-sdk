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
        public ListSmsRequestSortTypes? Sort { get; set; }
        [QueryParam]
        public int Limit { get; set; } = 20;

    }

    public enum ListSmsRequestStatusTypes
    {
        Completed,
        Draft,
        Failed,
        Warning,
        Queued
    }

    public enum ListSmsRequestSortTypes
    {
        Asc,
        Desc
    }
}
