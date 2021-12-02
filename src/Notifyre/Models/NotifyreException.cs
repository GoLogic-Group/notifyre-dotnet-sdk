using System;
using System.Collections.Generic;

namespace Notifyre
{
    public class NotifyreException : Exception
    {
        public bool Success { get; set; }

        public int StatusCode { get; set; }

        public List<string> Errors { get; set; }

        internal NotifyreException() { }

        internal NotifyreException(string message) : base(message) { }

        internal NotifyreException(string message, Exception err) : base(message, err) { }
    }
}
