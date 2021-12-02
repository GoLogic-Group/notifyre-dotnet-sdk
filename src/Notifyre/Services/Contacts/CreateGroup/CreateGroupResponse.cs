using System;

namespace Notifyre
{
    public class CreateGroupResponse
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public long CreatedDateUtc { get; set; }
    }
}
