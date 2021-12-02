using Notifyre.Infrastructure.Annotations;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Notifyre.Services.Fax.FaxSend.SubmitFax.SendFax
{
    public class SendFaxRequest
    {

        [BodyParam]
        public SubmitFax Faxes { get; set; }
        [BodyParam]
        public string TemplateName { get; set; }

        public class SubmitFax
        {
            [BodyParam]
            public List<FaxRecipient> Recipients { get; set; }

            [BodyParam]
            public string SendFrom { get; set; }

            [BodyParam]
            public bool IsHighQuality { get; set; }

            [BodyParam]
            public string ClientReference { get; set; }

            [BodyParam]
            public string Subject { get; set; }

            [BodyParam]
            public List<string> Files { get; set; } = new List<string>();

            [BodyParam]
            private List<SubmittedDocument> Documents { get; set; } = new List<SubmittedDocument>();

            [BodyParam]
            public string Header { get; set; }

            [BodyParam]
            public string SenderID { get; set; }

            [BodyParam]
            public long? ScheduledDate { get; set; }

        }

        public enum SubmitFaxRequestRecipientValueTypes
        {
            fax_number,
            contact,
            group
        }

        public SendFaxRequest(SubmitFaxRequest request, Guid[] ids)
        {
            TemplateName = request.TemplateName;
            Faxes = new SubmitFax()
            {
                Recipients = request.Faxes.Recipients,
                SendFrom = request.Faxes.SendFrom,
                IsHighQuality = request.Faxes.IsHighQuality,
                ClientReference = request.Faxes.ClientReference,
                Subject = request.Faxes.Subject,
                Files = ids.Select(x => x.ToString()).ToList(),
                Header = request.Faxes.Header,
                SenderID = request.Faxes.SenderID,
                ScheduledDate = request.Faxes.ScheduledDate,
            };
        }
    }
}