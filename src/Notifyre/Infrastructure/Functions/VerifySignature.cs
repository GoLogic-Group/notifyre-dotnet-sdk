using System;

namespace Notifyre.Infrastructure.Functions
{
    public static class VerifySignature
    {
        public static bool Execute(string signatureHeader, string payloadJson, string signingSecret, long timeToleranceSec = 300)
        {
            string timestamp = string.Empty;
            string signature = string.Empty;

            // Extract timestamp and signature
            var elements = signatureHeader.Split(',');
            foreach (var element in elements)
            {
                if (element.Split('=')[0] == "t")
                {
                    timestamp = element.Split('=')[1];
                }
                if (element.Split('=')[0] == "v")
                {
                    signature = element.Split('=')[1];
                }
            }

            // Ensure timestamp provided
            if (string.IsNullOrEmpty(timestamp))
            {
                throw new NotifyreException("Empty signature timestamp");
            }

            // Ensure signature provided
            if (string.IsNullOrEmpty(signature))
            {
                throw new NotifyreException("Empty signature");
            }

            // Verify timestamp and current time difference to protect against timing attacks
            if ((DateTime.UtcNow - ConvertDateTime.UnixTimeStampToDateTime(Convert.ToDouble(timestamp))).TotalSeconds > timeToleranceSec)
            {
                throw new NotifyreException("Signature timestamp expired");
            }

            string signedPayload = $"{timestamp}.{payloadJson}";
            string expectedSignature = GenerateSignature.Execute(signedPayload, signingSecret);

            if (expectedSignature.Equals(signature))
            {
                return true;
            }
            else
            {
                throw new NotifyreException("Invalid signature");
            }
        }
    }
}
