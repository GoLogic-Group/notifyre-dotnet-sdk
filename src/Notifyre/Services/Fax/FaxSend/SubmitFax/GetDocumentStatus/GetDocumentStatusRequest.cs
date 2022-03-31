using Notifyre.Infrastructure.Annotations;

namespace Notifyre.Services.Fax.FaxSend.SubmitFax.GetDocumentStatus
{
    internal class GetDocumentStatusRequest
    {
        [RouteParam]
        public string FileName { get; set; }

    }
}