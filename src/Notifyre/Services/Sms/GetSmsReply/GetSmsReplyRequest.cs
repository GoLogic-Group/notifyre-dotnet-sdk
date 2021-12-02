using Notifyre.Infrastructure.Annotations;

namespace Notifyre
{
    public class GetSmsReplyRequest
    {
        [RouteParam]
        public string ReplyID { get; set; }

    }
}
