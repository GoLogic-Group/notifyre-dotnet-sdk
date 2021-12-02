using Notifyre.Infrastructure.Annotations;
using System;
using System.Collections.Generic;

namespace Notifyre
{
    public class RemoveContactsFromGroupRequest
    {
        [BodyParam]
        public List<Guid> Contacts { get; set; }

        [BodyParam]
        public Guid GroupID { get; set; }
    }
}
