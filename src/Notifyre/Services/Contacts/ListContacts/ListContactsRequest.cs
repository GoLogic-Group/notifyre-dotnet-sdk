using Notifyre.Infrastructure.Annotations;
using System;
using System.Collections.Generic;

namespace Notifyre
{
    public class ListContactsRequest
    {
        private string searchQuery = "";

        [BodyParam]
        public string SearchQuery { get => searchQuery ?? ""; set => searchQuery = value; }
        [BodyParam]
        public int Page { get; set; }

        [BodyParam]
        public int Limit { get; set; }

        [BodyParam]
        public ListGroupsContactNumberType Type { get; set; }

        [BodyParam]
        public string SortBy { get; set; } = "name";

        [BodyParam]
        public ListContactsRequestSortTypes SortDir { get; set; } = ListContactsRequestSortTypes.asc;

        [BodyParam]
        public List<Guid> GroupIDs { get; set; }

        [BodyParam]
        public bool IncludeUnsubscribed { get; set; }
    }

    public enum ListContactsRequestSortTypes
    {
        asc,
        desc
    }
    public enum ListGroupsContactNumberType
    {
        FaxNumber,
        MobileNumber
    }
}
