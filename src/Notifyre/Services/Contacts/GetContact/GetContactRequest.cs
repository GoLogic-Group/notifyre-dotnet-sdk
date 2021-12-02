using Notifyre.Infrastructure.Annotations;
using System;

namespace Notifyre
{
    public class GetContactRequest
    {
        [RouteParam]
        public Guid ContactID { get; set; }
    }
}
