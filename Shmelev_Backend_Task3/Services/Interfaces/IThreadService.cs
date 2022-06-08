using System.Threading.Tasks;

namespace Shmelev_Backend_Task3
{
    public interface IThreadService
    {
        Task CreateThread(Thread thread);

        Task RemoveThread(int id);

        Task EditThread(int id, ThreadEditModel model);

        Task<Thread> GetThread(int id);

        Task<bool>IsAuthor(string userId, int threadId);
    }
}
