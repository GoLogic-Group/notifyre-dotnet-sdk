using Notifyre.Infrastructure.Annotations;
using System;
using System.Collections.Generic;

namespace Notifyre
{
    public class DeleteGroupsRequest
    {
        [BodyParam]
        public List<Guid> Groups { get; set; }

        [BodyParam]
        public bool IncludeContacts { get; set; }

    }
}
