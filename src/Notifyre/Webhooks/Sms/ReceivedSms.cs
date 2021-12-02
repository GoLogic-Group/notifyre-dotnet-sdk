using System;

namespace Notifyre.Webhooks.Sms
{
    public class ReceivedSms
    {
        public Guid RecipientID { get; set; }
        public string RecipientNumber { get; set; }
        public string SenderNumber { get; set; }
        public string ReplyID { get; set; }
        public string Message { get; set; }
        public long ReceivedDateUtc { get; set; }
    }
}
