using Notifyre.Infrastructure.Annotations;
using System;

namespace Notifyre
{
    public class GetSentSmsRequest
    {
        [RouteParam]
        public Guid MessageID { get; set; }
        [RouteParam]
        public Guid RecipientID { get; set; }

    }
}
