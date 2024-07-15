using Notifyre.Infrastructure.Annotations;

namespace Notifyre
{
    public class GetSmsReplyRequestV2
    {
        [RouteParam]
        public string ReplyID { get; set; }

    }
}
