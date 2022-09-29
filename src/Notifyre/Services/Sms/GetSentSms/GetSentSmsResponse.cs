using System;
using System.Collections.Generic;

namespace Notifyre
{
    public class GetSentSmsResponse
    {
        public Guid ID { get; set; }

        public string FriendlyID { get; set; }

        public string AccountID { get; set; }

        public string CreatedBy { get; set; }

        public List<RecipientDto> Recipients { get; set; }

        public string Status { get; set; }

        public long TotalCost { get; set; }

        public Dictionary<string, string> Metadata { get; set; }

        public long? CreatedDateUtc { get; set; }

        public long? SubmittedDateUtc { get; set; }

        public long? CompletedDateUtc { get; set; }

        public long? LastModifiedDateUtc { get; set; }

        public class RecipientDto
        {
            public Guid ID { get; set; }

            public string ToNumber { get; set; }

            public string FromNumber { get; set; }

            public string Message { get; set; }

            public decimal Cost { get; set; }

            public int MessageParts { get; set; }

            public decimal CostPerPart { get; set; }

            public string Status { get; set; }

            public string StatusMessage { get; set; }

            public string DeliveryStatus {get; set; }

            public long? QueuedDateUtc { get; set; }

            public long? CompletedDateUtc { get; set; }
        }
    }
}
