using Notifyre.Infrastructure.Annotations;
using System;

namespace Notifyre
{
    public class UpdateGroupRequest
    {
        [RouteParam]
        public Guid ID { get; set; }

        [BodyParam]
        public string Name { get; set; }
    }
}
