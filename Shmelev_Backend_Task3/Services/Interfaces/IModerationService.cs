using System.Threading.Tasks;

namespace Shmelev_Backend_Task3.Services.Interfaces
{
    public interface IModerationService
    {
    }

    public class ModerationService
    {
        IUserService _userService;
        public ModerationService(IUserService service)
        {
            _userService = service;
        }

        //public async Task<bool> ModeratesBoard(ForumUser user, ModeratedBoard board)
        //{
            
        //}
    }
}
