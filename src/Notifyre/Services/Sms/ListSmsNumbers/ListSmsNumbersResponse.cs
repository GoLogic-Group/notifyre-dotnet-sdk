using System;
using System.Collections.Generic;

namespace Notifyre
{
    public class ListSmsNumbersResponse
    {
        public List<SmsNumberDto> SmsNumbers { get; set; }
        public List<SmsSenderIdDto> SmsSenderIds { get; set; }

        public class SmsNumberDto
        {
            public Guid ID { get; set; }
            public int CountryCode { get; set; }
            public ulong AssignedNumber { get; set; }
            public string E164 { get; set; }
            public string Provider { get; set; }
            public string Status { get; set; }
            public string SubscriptionID { get; set; }
            public long? CreatedDateUtc { get; set; }
            public long? LastModifiedDateUtc { get; set; }
            public long StartDateUtc { get; set; }
            public long? FinishDateUtc { get; set; }
            public string CampaignID { get; set; }
        }

        public class SmsSenderIdDto
        {
            public Guid ID { get; set; }
            public string Name { get; set; }
            public string Status { get; set; }
            public long? CreatedDateUtc { get; set; }
            public long? LastModifiedDateUtc { get; set; }
        }
    }
}