using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shmelev_Backend_Task3
{
    public interface IUserService
    {
        Task<ForumUser> GetUserById(string userId);
        Task<string> GetUserId(ClaimsPrincipal user);
        Task<ForumUser> GetUserByUserName(string userName);

        Task<ForumUser> GetUserByEmail(string email);

        Task<List<ForumUser>> GetAllUsers();

        Task AssignModerator(string userId, int boardId);

        bool HasModerator(int boardId, string userId);

        Task<string> GetThreadAuthor(int threadId);
        Task<string> GetPostAuthor(int postId);

        Task<ForumUser> GetThreadAuthorUser(int threadId);

        List<ForumUser> GetAllUsers(Func<ForumUser, bool> predicate);

        List<ForumUser> GetUsersWithRole(string role);
    }
}
