using System;
using System.Collections.Generic;

namespace Notifyre
{
    public class ListSentFaxesResponse
    {
        public List<SentFax> Faxes { get; set; }

        public class SentFax
        {
            public Guid ID { get; set; }
            public string FriendlyID { get; set; }
            public Guid RecipientID { get; set; }
            public string FromNumber { get; set; }
            public string To { get; set; }
            public string Reference { get; set; }
            public long CreatedDateUtc { get; set; }
            public long? QueuedDateUtc { get; set; }
            public long? LastModifiedDateUtc { get; set; }
            public bool HighQuality { get; set; }
            public int Pages { get; set; }
            public string Status { get; set; }
            public string FailedMessage { get; set; }
        }
    }
}
