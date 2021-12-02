using Notifyre.Constants;
using Notifyre.Infrastructure.Annotations;
using System;
using System.Collections.Generic;

namespace Notifyre
{
    public class SubmitFaxRequest
    {
        [BodyParam]
        public SubmitFax Faxes { get; set; }
        [BodyParam]
        public string TemplateName { get; set; }

    }

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
        internal List<string> Files { get; set; } = new List<string>();

        [BodyParam]
        public List<SubmittedDocument> Documents { get; set; } = new List<SubmittedDocument>();

        [BodyParam]
        public string Header { get; set; }

        [BodyParam]
        public string SenderID { get; set; }

        [BodyParam]
        public long? ScheduledDate { get; set; }

    }

    public class FaxRecipient
    {
        [BodyParam(typeof(RecipientValueTypes))]
        public SubmitFaxRequestRecipientValueTypes Type { get; set; }
        [BodyParam]
        public string Value { get; set; }
    }

    public class SubmittedDocument
    {
        public SubmittedDocument()
        {
            ClientReference = Guid.NewGuid();
        }

        /// <summary>
        /// The document will be uploaded to the server for conversion
        /// </summary>
        /// <param name="clientReference">This can be set to track the file if exceptions in the conversion process occur</param>
        /// <param name="base64">A base64 encoded string of the file consistent with the content type</param>
        /// <param name="contentType">Refer to SubmitFaxRequestContentTypes enum</param>
        public SubmittedDocument(
            Guid clientReference,
            string base64,
            SubmitFaxRequestContentTypes contentType
        )
        {
            ClientReference = clientReference;
            Base64Str = base64;
            ContentType = contentType;
        }

        /// <summary>The ClientReference can be set to track the file in case of conversion failures</summary>
        public Guid ClientReference { get; set; } // This can be set by the client to track the file in case of exception. It does not get sent to the server

        [BodyParam]
        public string Base64Str { get; set; }

        [BodyParam(typeof(FileContentType))]
        public SubmitFaxRequestContentTypes ContentType { get; set; }
    }

    public enum SubmitFaxRequestRecipientValueTypes
    {
        FaxNumber,
        Contact,
        Group
    }

    internal static class RecipientValueTypes
    {
        public const string FaxNumber = "fax_number";
        public const string Contact = "contact";
        public const string Group = "Group";

    }

    public enum SubmitFaxRequestContentTypes
    {
        Bmp,
        Gif,
        Jpeg,
        Jpg,
        Png,
        Docx,
        Dotx,
        Doc,
        Xlsx,
        Xltx,
        Xls,
        Pptx,
        Potx,
        Ppsx,
        Ppt,
        Rtf,
        Txt,
        Html,
        Pdf,
        Ps,
        Tiff,
        Tif
    }
}