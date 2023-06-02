using System.Collections.Generic;

namespace Notifyre
{
    public class ListFaxPricesResponse
    {
        public List<CountryPrice> Prices { get; set; }

        public class CountryPrice
        {
            public string CountryCode { get; set; }
            public string CountryName { get; set; }
            public string Prefix { get; set; }
            public decimal Price { get; set; }
            public string Currency { get; set; }
        }
    }
}
