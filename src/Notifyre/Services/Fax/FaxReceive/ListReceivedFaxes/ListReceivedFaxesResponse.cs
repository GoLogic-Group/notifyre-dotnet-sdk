namespace Notifyre
{
    public class ListReceivedFaxesResponse
    {
        public string ID { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public long Timestamp { get; set; }
        public string Status { get; set; }
        public int Pages { get; set; }
        public double Duration { get; set; }
        public bool Read { get; set; }
    }
}
