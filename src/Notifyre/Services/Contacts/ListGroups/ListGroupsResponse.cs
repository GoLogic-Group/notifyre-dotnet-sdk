using System;
using System.Collections.Generic;

namespace Notifyre
{
    public class ListGroupsResponse
    {
        public List<GroupDto> Groups { get; set; }

        public class GroupDto
        {
            public Guid? ID { get; set; }

            public string Name { get; set; }

            public long? CreatedDateUtc { get; set; }

            public long ContactsCount { get; set; }
        }
    }
}
