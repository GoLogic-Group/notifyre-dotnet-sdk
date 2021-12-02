using System.Collections.Generic;

namespace Notifyre
{
    public class SubmitSmsResponse
    {
        public string SmsMessageID { get; set; }

        public string FriendlyID { get; set; }

        public List<InvalidNumber> InvalidToNumbers { get; set; }

        public class InvalidNumber
        {
            public string Number { get; set; }

            public string Message { get; set; }
        }
    }
}
