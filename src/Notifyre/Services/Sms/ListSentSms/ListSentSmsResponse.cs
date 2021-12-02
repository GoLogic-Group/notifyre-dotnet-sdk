using System;
using System.Collections.Generic;

namespace Notifyre
{
    public class ListSentSmsResponse
    {
        public List<SmsMessageDto> SmsMessages { get; set; }

        public class SmsMessageDto
        {
            public Guid ID { get; set; }

            public string FriendlyID { get; set; }

            public string AccountID { get; set; }

            public string CreatedBy { get; set; }

            public RecipientDto Recipient { get; set; }

            public string Status { get; set; }

            public long TotalCost { get; set; }

            public long? CreatedDateUtc { get; set; }

            public long? SubmittedDateUtc { get; set; }

            public long? CompletedDateUtc { get; set; }

            public long? LastModifiedDateUtc { get; set; }

            public class RecipientDto
            {
                public Guid ID { get; set; }

                public string ToNumber { get; set; }

                public string FromNumber { get; set; }

                public decimal Cost { get; set; }

                public int MessageParts { get; set; }

                public decimal CostPerPart { get; set; }

                public string Status { get; set; }

                public long? QueuedDateUtc { get; set; }

                public long? CompletedDateUtc { get; set; }
            }
        }
    }
}
