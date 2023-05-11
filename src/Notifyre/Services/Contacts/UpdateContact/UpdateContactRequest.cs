using Notifyre.Infrastructure.Annotations;
using System;
using System.Collections.Generic;

namespace Notifyre
{
    public class UpdateContactRequest
    {
        [RouteParam]
        public Guid ID { get; set; }

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
        public bool Unsubscribed { get; set; }

        [BodyParam]
        public List<CustomField> CustomFields { get; set; } = new List<CustomField>();
    }
}
