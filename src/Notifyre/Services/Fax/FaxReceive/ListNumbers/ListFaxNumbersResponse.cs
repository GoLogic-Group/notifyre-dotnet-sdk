using System.Collections.Generic;

namespace Notifyre
{
    public class ListFaxNumbersResponse
    {
        public List<FaxNumber> Numbers { get; set; }

        public class FaxNumber
        {
            public int ID { get; set; }
            public int CountryCode { get; set; }
            public string Region { get; set; }
            public ulong AssignedNumber { get; set; }
            public string E164 { get; set; }
            public string Status { get; set; }
        }
    }
}