using System;

namespace Notifyre.Infrastructure.Annotations
{
    public class DateTimeFormatAttribute : Attribute
    {
        public string Format { get; set; }

        public DateTimeFormatAttribute(string format)
        {
            Format = format;
        }
    }
}
