﻿using System;

namespace Notifyre
{
    public class GetSmsReplyResponseV2
    {
        public Guid RecipientID { get; set; }
        public string FriendlyID { get; set; }

        public string RecipientNumber { get; set; }

        public string SenderNumber { get; set; }

        public string ReplyID { get; set; }

        public string Message { get; set; }
        public string Subject  { get; set; } = string.Empty;

        public long ReceivedDateUtc { get; set; }

        public ContactDto ContactDetails { get; set; }

        public class ContactDto
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }

            public string Organization { get; set; }

        }
    }
}
