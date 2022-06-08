using Microsoft.AspNetCore.Identity;
using System.Collections;
using System.Collections.Generic;

namespace Shmelev_Backend_Task3
{
    public class ForumUser:IdentityUser
    {
        public ICollection<ModeratedBoard> ModeratedBoards { get; set; }
        public ICollection<Thread> AuthoredThreads { get; set; }
        public ICollection<Post> AuthoredPosts{ get; set; }

        public ICollection<ForumUserRole> ForumUserRoles { get; set; }
    }

    public class ForumRole : IdentityRole
    {
        public ICollection<ForumUserRole> ForumUserRoles { get; set; }


    }

    public class ForumUserRole : IdentityUserRole<string>
    {
        public virtual ForumUser User { get; set; } 
        public virtual ForumRole Role { get; set; }

    }
}
