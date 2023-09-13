using Notifyre.Infrastructure.Annotations;

namespace Notifyre
{
    public class DownloadMmsReplyRequest
    {
        [RouteParam]
        public string ReplyID { get; set; }
    }
}
