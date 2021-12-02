using System;

namespace Notifyre.Services.Fax.FaxSend.SubmitFax.GetDocumentStatus
{
    public class GetDocumentStatusResponse
    {
        public Guid? ID { get; set; }

        public string Status { get; set; }

        public int? Pages { get; set; }

        public string FileName { get; set; }

    }
}
