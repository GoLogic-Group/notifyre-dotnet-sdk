using Notifyre.Infrastructure.Annotations;
using System;
using System.Collections.Generic;

namespace Notifyre
{
    public class AddContactsToGroupsRequest
    {
        [BodyParam]
        public List<Guid> Contacts { get; set; }

        [BodyParam]
        public List<Guid> Groups { get; set; }
    }
}
