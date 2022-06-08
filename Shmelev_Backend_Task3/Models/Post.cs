using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shmelev_Backend_Task3
{
    public class Post
    {
        public int Id { get; set; }

        public Thread Thread { get; set; }
        public int ThreadId { get; set; }

        public ForumUser Author { get; set; }
        public string AuthorId { get; set; }

        public string Text { get; set; }

        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }


        public ICollection<Attachment> Attachments { get; set; }
    }
}
