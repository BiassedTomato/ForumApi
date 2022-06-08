using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shmelev_Backend_Task3
{
    public class ModeratedBoard
    {
        public int Id { get; set; }
        public int BoardId { get; set; }
        public Board Board { get; set; }
        public ICollection<ForumUser> Moderators { get; set; }=new List<ForumUser>();
    }
}
