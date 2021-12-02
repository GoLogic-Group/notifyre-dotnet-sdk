using System;

namespace Notifyre.Infrastructure.Utils
{
    public static class DateTimeUtil
    {
        public static long ToUnixTimeStamp(this DateTime t) => (long)t.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

    }
}
