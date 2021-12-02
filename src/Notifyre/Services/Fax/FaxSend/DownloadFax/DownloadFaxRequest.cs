using System;
using Notifyre.Infrastructure.Annotations;

namespace Notifyre
{
    public class DownloadFaxRequest
    {
        public Guid ID { get; set; }
        [QueryParam(typeof(FileTypes))]
        public DownloadFaxRequestFileTypes FileType { get; set; }

    }

    public enum DownloadFaxRequestFileTypes
    {
        Pdf,
        Tiff
    }

    internal static class FileTypes
    {
        public const string Pdf = "pdf";
        public const string Tiff = "tiff";
    }
}
