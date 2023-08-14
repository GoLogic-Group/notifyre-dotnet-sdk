using System.Collections.Generic;

namespace Notifyre
{
    public class DownloadMmsReplyResponse
    {
        public List<MmsDocument> DocumentList { get; set; } = new List<MmsDocument>();

        public class MmsDocument
        {
            public string Base64Str { get; set; }
            public string Type { get; set; }

        }
    }
}
