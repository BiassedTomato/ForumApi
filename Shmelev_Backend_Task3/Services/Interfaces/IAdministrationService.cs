using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shmelev_Backend_Task3
{
    public interface IAdministrationService
    {
        Task AssignModerator(string userId, int boardId);
        bool IsAdmin(ClaimsPrincipal user);
    }

    public class AdministrationService : IAdministrationService
    {
        IUserService _userService;
        ForumContext _context;
        public AdministrationService(IUserService userService, ForumContext forumContext)
        {
            _userService = userService;
            _context = forumContext;
        }

        public async Task AssignModerator(string userId, int boardId)
        {
            var moderatedBoard = new ModeratedBoard()
            {
                BoardId = boardId,

            };

            if (!moderatedBoard.Moderators.Any(x => x.Id == userId))
            {
                moderatedBoard.Moderators.Add(await _userService.GetUserById(userId));

                await _context.ModeratedBoards.AddAsync(moderatedBoard);
                await _context.SaveChangesAsync();
            }

        }

        public bool IsAdmin(ClaimsPrincipal user) => user.IsInRole("Admin");
    }
}
