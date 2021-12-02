using Notifyre.Infrastructure.Annotations;
using System;
using System.Collections.Generic;

namespace Notifyre
{
    public class DeleteContactsRequest
    {
        [BodyParam]
        public List<Guid> Contacts { get; set; }

    }
}
