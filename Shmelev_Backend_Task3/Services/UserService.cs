using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shmelev_Backend_Task3
{
    public class UserService : IUserService
    {
        UserManager<ForumUser> _manager;

        ForumContext _context;

        public UserService(ForumContext ctx, UserManager<ForumUser> manager)
        {
            _context= ctx;
            _manager = manager;
        }

        public async Task<ForumUser> GetUser(ClaimsPrincipal user)
        {
            return await _manager.GetUserAsync(user);
        }

        public async Task<string> GetUserId(ClaimsPrincipal user)
        {
            var forumUser = await GetUser(user);

            var id= forumUser.Id;

            return forumUser.Id;
        }

        public Task<ForumUser> GetUserByEmail(string email)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ForumUser> GetUserById(string userId)
        {
            return await _manager.Users.FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<ForumUser> GetUserByUserName(string userName)
        {
            return await _manager.Users.FirstOrDefaultAsync(x => x.UserName == userName);
        }

        public async Task<List<ForumUser>> GetAllUsers()
        {
            return await _manager.Users.ToListAsync();
        }

        public List<ForumUser> GetAllUsers(Func<ForumUser, bool> predicate)
        {
            return _manager.Users.Where(predicate).ToList();
        }

        public bool HasModerator(int boardId,string userId)
        {

            return _context.ModeratedBoards
                .Include(x => x.Moderators)
                .Any(x => x.BoardId == boardId && x.Moderators.Any(y => y.Id == userId));


        }

        public async Task AssignModerator(string userId,int boardId)
        {
            var moderatedBoard= new ModeratedBoard()
            {
                BoardId = boardId,

            };

            moderatedBoard.Moderators.Add(await GetUserById(userId));

            await _context.ModeratedBoards.AddAsync(moderatedBoard);
            await _context.SaveChangesAsync();
        }

        public bool IsAdmin(ClaimsPrincipal user) => user.IsInRole("Admin");

        public async Task<string> GetThreadAuthor(int threadId)
        {
            var thread = await _context.Threads.FirstOrDefaultAsync(x => x.Id == threadId);

            if (thread == null)
                throw new System.Exception("No thread found");

            return thread.AuthorId;
        }

        public async Task<ForumUser> GetThreadAuthorUser(int threadId)
        {
            var thread = await _context.Threads.FirstAsync(x => x.Id == threadId);

            if (thread == null)
                throw new System.Exception("No thread found");

            return await GetUserById(thread.AuthorId);
        }

        public async Task<string> GetPostAuthor(int postId)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == postId);

            if (post == null)
                throw new NullReferenceException("No post found");

            return post.AuthorId;
        }

        public List<ForumUser> GetUsersWithRole(string role)
        {
            return  _context.Users
                .Include(x => x.ForumUserRoles)
                .ThenInclude(x => x.Role)
                .Where(x => x.ForumUserRoles
                .Any(x => x.Role.Name == role))
                .ToList();
        }
    }
}
