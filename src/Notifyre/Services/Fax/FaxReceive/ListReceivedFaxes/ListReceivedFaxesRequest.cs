using Notifyre.Infrastructure.Annotations;

namespace Notifyre
{
    public class ListReceivedFaxesRequest
    {
        [QueryParam] public string ToNumber { get; set; } = string.Empty;

        [QueryParam] public int Limit { get; set; } = 10;

        [QueryParam] public int Skip { get; set; } = 0;

        [QueryParam] public long? FromDate { get; set; }

        [QueryParam] public long? ToDate { get; set; }

        [QueryParam]
        public ListReceivedFaxesRequestSortTypes Sort { get; set; } = ListReceivedFaxesRequestSortTypes.Desc;
    }

    public enum ListReceivedFaxesRequestSortTypes
    {
        Asc,
        Desc
    }
}