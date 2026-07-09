using Notifyre.Infrastructure.Annotations;

namespace Notifyre
{
    public class DownloadAndDeleteReceivedFaxRequest
    {
        [RouteParam]
        public int FaxID { get; set; }

    }
}
