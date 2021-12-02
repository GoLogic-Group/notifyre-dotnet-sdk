using System;
using System.Collections.Generic;

namespace Notifyre
{
    public class GetContactResponse
    {
        public Guid ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Organization { get; set; }

        public string Email { get; set; }

        public string FaxNumber { get; set; }

        public string MobileNumber { get; set; }

        public List<GroupDto> Groups { get; set; }

        public List<CustomFieldDto> CustomFields { get; set; }

        public class GroupDto
        {
            public Guid ID { get; set; }

            public string Name { get; set; }

            public long CreatedDateUtc { get; set; }
        }

        public class CustomFieldDto
        {
            public Guid ID { get; set; }

            public string Key { get; set; }

            public string Value { get; set; }
        }
    }
}
