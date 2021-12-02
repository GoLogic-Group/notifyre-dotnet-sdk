using Notifyre.Infrastructure.Annotations;

namespace Notifyre
{
    public class ListGroupsRequest
    {
        [QueryParam]
        public string SearchQuery { get; set; }

        [QueryParam(typeof(SortByTypes))]
        public ListGroupsRequestSortByTypes SortBy { get; set; }

        [QueryParam]
        public ListGroupsRequestSortTypes SortDir { get; set; }
    }

    internal static class SortByTypes
    {
        public const string Name = "name";
        public const string DateCreated = "date_created";
    }

    public enum ListGroupsRequestSortByTypes
    {
        Name,
        DateCreated
    }

    public enum ListGroupsRequestSortTypes
    {
        asc,
        desc
    }
}
