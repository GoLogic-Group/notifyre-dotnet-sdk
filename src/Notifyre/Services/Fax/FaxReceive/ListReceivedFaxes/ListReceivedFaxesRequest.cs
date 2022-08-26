using Notifyre.Infrastructure.Annotations;

namespace Notifyre
{
    public class ListReceivedFaxesRequest
    {
        [QueryParam]
        public string ToNumber { get; set; }

        [QueryParam]
        public int? Limit { get; set; }

        [QueryParam]
        public int? Skip { get; set; }

    }
}
