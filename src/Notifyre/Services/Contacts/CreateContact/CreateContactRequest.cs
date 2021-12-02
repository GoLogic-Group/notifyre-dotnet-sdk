using Notifyre.Infrastructure.Annotations;
using System;
using System.Collections.Generic;

namespace Notifyre
{
    public class CreateContactRequest
    {
        [BodyParam]
        public string FirstName { get; set; }

        [BodyParam]
        public string LastName { get; set; }

        [BodyParam]
        public string Organization { get; set; }

        [BodyParam]
        public string Email { get; set; }

        [BodyParam]
        public string FaxNumber { get; set; }

        [BodyParam]
        public string MobileNumber { get; set; }

        [BodyParam]
        public List<Guid> Groups { get; set; } = new List<Guid>();

        [BodyParam]
        public string GroupName { get; set; }

        [BodyParam]
        public List<CustomField> CustomFields { get; set; } = new List<CustomField>();
    }

    public class CustomField
    {
        [BodyParam]
        public string Key { get; set; }

        [BodyParam]
        public string Value { get; set; }
    }
}
