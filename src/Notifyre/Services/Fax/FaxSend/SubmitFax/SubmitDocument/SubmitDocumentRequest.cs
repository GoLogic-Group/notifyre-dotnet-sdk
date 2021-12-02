using Notifyre.Constants;
using Notifyre.Infrastructure.Annotations;
using System;
using System.Collections.Generic;

namespace Notifyre.Services.Fax.FaxSend.SubmitFax.SubmitDocument
{
    public class SubmitDocumentRequest
    {
        [BodyParam]
        public string Base64Str { get; set; }

        [BodyParam(typeof(FileContentType))]
        public SubmitFaxRequestContentTypes ContentType { get; set; }

    }
}