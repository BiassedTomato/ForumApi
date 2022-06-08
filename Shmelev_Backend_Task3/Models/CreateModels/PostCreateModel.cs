using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shmelev_Backend_Task3
{
    public class PostCreateModel
    {
        [StringLength(999,MinimumLength =2)]
        public string Text { get; set; }

        public int ThreadId { get; set; }

        public List<IFormFile> Files { get; set; } = new List<IFormFile>();
    }
}
