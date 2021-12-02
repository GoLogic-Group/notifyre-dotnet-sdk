using Notifyre.Infrastructure.Annotations;

namespace Notifyre
{
    public class DownloadReceivedFaxRequest
    {
        [RouteParam]
        public int FaxID { get; set; }

    }
}
