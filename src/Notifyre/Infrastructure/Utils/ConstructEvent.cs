using Newtonsoft.Json;
using Notifyre.Constants;
using Notifyre.Infrastructure.Functions;
using Notifyre.Models;
using Notifyre.Webhooks.Fax;
using Notifyre.Webhooks.Sms;

namespace Notifyre.Infrastructure.Utils
{
    public static class ConstructEvent
    {
        public static NotifyreEvent Execute(string json, string signature, string endpointSecret, long timeToleranceSec = 300)
        {
            // Verify signature
            if (!VerifySignature.Execute(signature, json, endpointSecret, timeToleranceSec))
            {
                throw new NotifyreException($"Invalid signature");
            }

            // Deserialize event
            var jsonResult = JsonConvert.DeserializeObject<dynamic>(json);
            string eventName = jsonResult.Event;
            long timestamp = jsonResult.Timestamp;

            // Determine payload
            if (eventName == EventType.FaxReceived)
            {
                return new NotifyreEvent<ReceivedFax>
                {
                    Event = eventName,
                    Timestamp = timestamp,
                    Payload = JsonConvert.DeserializeObject<ReceivedFax>(JsonConvert.SerializeObject(jsonResult.Payload))
                };
            }

            if (eventName == EventType.FaxSent)
            {
                return new NotifyreEvent<SentFax>
                {
                    Event = eventName,
                    Timestamp = timestamp,
                    Payload = JsonConvert.DeserializeObject<SentFax>(JsonConvert.SerializeObject(jsonResult.Payload))
                };
            }

            if (eventName == EventType.SmsReceived)
            {
                return new NotifyreEvent<ReceivedSms>
                {
                    Event = eventName,
                    Timestamp = timestamp,
                    Payload = JsonConvert.DeserializeObject<ReceivedSms>(JsonConvert.SerializeObject(jsonResult.Payload))
                };
            }

            if (eventName == EventType.SmsSent)
            {
                return new NotifyreEvent<SentSms>
                {
                    Event = eventName,
                    Timestamp = timestamp,
                    Payload = JsonConvert.DeserializeObject<SentSms>(JsonConvert.SerializeObject(jsonResult.Payload))
                };
            }

            throw new NotifyreException($"Invalid webhook event");
        }
    }
}
