namespace Notifyre.Models
{
    public class NotifyreEvent
    {
        public string Event { get; set; }

        public long Timestamp { get; set; }
    }

    public class NotifyreEvent<T> : NotifyreEvent
    {
        public T Payload { get; set; }
    }
}
