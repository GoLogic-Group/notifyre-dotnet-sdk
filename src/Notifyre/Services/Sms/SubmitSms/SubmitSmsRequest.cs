using Notifyre.Infrastructure.Annotations;
using System.Collections.Generic;

namespace Notifyre
{
    public class SubmitSmsRequest
    {
        [BodyParam]
        public string Body { get; set; }
        [BodyParam]
        public List<SmsRecipient> Recipients { get; set; }
        [BodyParam]
        public string From { get; set; }
        [BodyParam]
        public long? ScheduledDate { get; set; }
        [BodyParam]
        public bool OptOutMessage { get; set; }
        [BodyParam]
        public bool AddUnsubscribeLink { get; set; }
        [BodyParam]
        public Dictionary<string, string> Metadata { get; set; }

    }

    public class SmsRecipient
    {
        [BodyParam(typeof(RecipientType))]
        public SubmitSmsRequestRecipientType Type { get; set; }
        [BodyParam]
        public string Value { get; set; }
    }

    public enum SubmitSmsRequestRecipientType
    {
        Group,
        Contact,
        MobileNumber
    }

    internal static class RecipientType
    {
        public const string MobileNumber = "mobile_number";
        public const string Group = "group";
        public const string Contact = "contact";
    }
}
