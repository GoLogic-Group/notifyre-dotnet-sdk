using System.Collections.Generic;

namespace Notifyre.Models
{
    public class BaseResponse<T>
    {
        public bool Success { get; set; }

        public int StatusCode { get; set; }

        public string Message { get; set; }

        public T Payload { get; set; }

        public List<string> Errors { get; set; }

        protected BaseResponse() { }

        internal BaseResponse(bool success, int statusCode, string message = null)
        {
            Success = success;
            StatusCode = statusCode;
            Message = message;
        }
    }
}
