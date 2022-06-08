using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shmelev_Backend_Task3
{
    public class Thread
    {
        public int Id { get; set; }

        public Board Board { get; set; }
        public int BoardId { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [StringLength(50, ErrorMessage = "The name has to be between 3 and 50 symbols long", MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 2)]
        public string Description { get; set; }

        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }

        public ICollection<Post> Posts { get; set; } = new List<Post>();

        public ForumUser Author { get; set; }
        public string AuthorId { get; set; }
    }
}
