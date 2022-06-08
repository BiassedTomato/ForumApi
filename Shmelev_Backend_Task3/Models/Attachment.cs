using System;

namespace Shmelev_Backend_Task3
{
    public class Attachment
    {
        public int Id { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }

        public DateTime Created { get; set; }

        public Post Post { get; set; }
        public int PostId { get; set; }

    }
}
