using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Shmelev_Backend_Task3
{
    public class RolesEditModel
    {
        public ICollection<ForumUser> Users { get; set; }
        public ICollection<Board> Boards { get; set; }

        public string SelectedUserId { get; set; }
        public int SelectedBoardId { get; set; }

        public SelectList SelectList { get; set; }

        
    }
}
